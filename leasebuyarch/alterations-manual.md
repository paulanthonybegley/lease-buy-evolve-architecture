# Alterations Manual

This document lists every code change required to get the Chapter 1 application
building and running correctly on .NET 10 with a local PostgreSQL database.

---

## Table of Contents

1. [Hotfix 1: requests.http Base URL](#hotfix-1-requestshttp-base-url)
2. [Hotfix 2: appsettings.Development.json Connection String](#hotfix-2-appsettingsdevelopmentjson-connection-string)
3. [Hotfix 3: InMemoryEventBus Scoped Service Resolution](#hotfix-3-inmemoryeventbus-scoped-service-resolution)
4. [Hotfix 4: CostComparisonDto Dapper Materialization](#hotfix-4-costcomparisondto-dapper-materialization)
5. [Summary of Changes](#summary-of-changes)

---

## Hotfix 1: requests.http Base URL

### Symptom

Clicking "Send Request" in VS Code returns `Connection refused`.

### Cause

The `requests.http` file used `@baseUrl = http://localhost:5000`, but the
application's `launchSettings.json` configures the HTTP profile on port **5013**:

```json
"applicationUrl": "http://localhost:5013"
```

### Fix

**File:** `requests.http` (line 2)

```diff
-@baseUrl = http://localhost:5000
+@baseUrl = http://localhost:5013
```

### Why 5013 and not 5000?

Port 5000 is the classic ASP.NET Core default, but .NET 10 templates randomise
the port per project to avoid conflicts when running multiple apps. The exact
port is defined in `Properties/launchSettings.json` under the `"http"` profile.

### How to find your port

```bash
grep applicationUrl Src/LeaseBuyArch/Properties/launchSettings.json
```

---

## Hotfix 2: appsettings.Development.json Connection String

### Symptom

Running `make run` fails immediately at startup:

```
Npgsql.NpgsqlException: nodename nor servname provided, or not known
```

### Cause

The `appsettings.Development.json` connection strings used `Host=postgres:5432`.

`postgres` is the Docker Compose service name — it resolves inside Docker's
internal DNS (when containers talk to each other). When running the app locally
with `dotnet run`, the OS tries to resolve `postgres` as a hostname and fails
because no such DNS record exists on your machine.

```json
{
  "ConnectionStrings": {
    "Leasing": "Host=postgres:5432;Database=leasebuy;..."
  }
}
```

### Fix

**File:** `Src/LeaseBuyArch/appsettings.Development.json`

```diff
-    "Leasing": "Host=postgres:5432;Database=leasebuy;..."
+    "Leasing": "Host=localhost:5432;Database=leasebuy;..."
-    "Purchasing": "Host=postgres:5432;Database=leasebuy;..."
+    "Purchasing": "Host=localhost:5432;Database=leasebuy;..."
-    "Comparison": "Host=postgres:5432;Database=leasebuy;..."
+    "Comparison": "Host=localhost:5432;Database=leasebuy;..."
-    "Vehicles": "Host=postgres:5432;Database=leasebuy;..."
+    "Vehicles": "Host=localhost:5432;Database=leasebuy;..."
```

All four connection strings (`Leasing`, `Purchasing`, `Comparison`, `Vehicles`)
need the same change.

### Docker vs Local Dual-Use

The `appsettings.json` (base, non-Development) intentionally keeps connection
strings **empty**:

```json
"ConnectionStrings": {
    "Leasing": "",
    "Purchasing": "",
    "Comparison": "",
    "Vehicles": ""
}
```

This acts as a safety net — if you forget a development override, the app
crashes immediately with a clear error instead of silently connecting to a
wrong database. But the Development override is specific to how you run:

| Run Mode | Host |
|---|---|
| `make docker-up` (Docker Compose) | `postgres` (Docker DNS) |
| `make run` (local dotnet) | `localhost` (your machine) |
| CI/CD pipeline | Depends on your CI PostgreSQL service name |

If you switch between Docker and local runs, you'll need to toggle this file.
A more robust approach is to use the `ASPNETCORE_CONNECTIONSTRINGS__LEASING`
environment variable to override at runtime without touching files.

### Prerequisite: PostgreSQL must be running

The app needs PostgreSQL to start (it runs EF Core migrations at startup).
Start it with Docker:

```bash
cd Src
docker compose up -d postgres
```

Verify it's ready:

```bash
docker compose ps postgres
# Should show "Up" and "(healthy)"
```

---

## Hotfix 3: InMemoryEventBus Scoped Service Resolution

### Symptom

Signing a lease returns `500 Internal Server Error`:

```json
{
  "StatusCode": 500,
  "Message": "Cannot resolve 'IEnumerable<INotificationHandler<LeaseSignedEvent>>'
               from root provider because it requires scoped service
               'VehiclesPersistence'."
}
```

### Root Cause

The `InMemoryEventBus` was registered as a **singleton** and injected `IMediator`
directly in its constructor:

```csharp
// BEFORE — BROKEN
internal sealed class InMemoryEventBus : IEventBus
{
    private readonly IMediator _mediator;

    public InMemoryEventBus(IMediator mediator) => _mediator = mediator;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct)
        where TEvent : IIntegrationEvent =>
        await _mediator.Publish(@event, ct);
}
```

Here's the chain of failure:

1. `InMemoryEventBus` is a singleton (created once, lives forever)
2. Because it's a singleton, its `IMediator` is resolved from the **root** DI scope
3. When `PublishAsync` is called, `IMediator` resolves handlers (`LeaseSignedEventHandler`)
   from the root scope (the one it was created in)
4. `LeaseSignedEventHandler` depends on `VehiclesPersistence` (an EF Core DbContext)
5. `VehiclesPersistence` is registered as **scoped** (created once per HTTP request)
6. You cannot resolve a scoped service from the root scope → `InvalidOperationException`

### Fix

**File:** `Src/LeaseBuyArch/Common/Events/EventBus/InMemory/InMemoryEventBus.cs`

Replace `IMediator` injection with `IServiceScopeFactory`:

```csharp
// AFTER — FIXED
internal sealed class InMemoryEventBus : IEventBus
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InMemoryEventBus(IServiceScopeFactory scopeFactory) =>
        _scopeFactory = scopeFactory;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct)
        where TEvent : IIntegrationEvent
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Publish(@event, ct);
    }
}
```

### How the Fix Works

Each call to `PublishAsync` now:

1. Creates a **new DI scope** (`IServiceScopeFactory.CreateScope()`)
2. Resolves `IMediator` from that scope (not the root)
3. MediatR resolves handlers using the same scope
4. Scoped services like `VehiclesPersistence` are created within this scope
5. The scope is disposed when `PublishAsync` completes

### Why Not Just Change to Scoped?

An alternative fix would be registering `IEventBus` as scoped:

```csharp
services.AddScoped<IEventBus, InMemoryEventBus>();
```

This was rejected because:

- The `LeaseSignedEventHandler` **also publishes an event**
  (`VehicleOwnershipRegisteredEvent`), creating a nested publish within the
  same handler. With a scoped bus, this re-enters the same scope, which is
  safe but less obvious than the scope-factory approach.
- Scope-factory keeps the bus as a singleton, which is semantically correct
  (the event bus is a service, not a per-request concern).
- The scope-factory pattern is the official ASP.NET Core recommendation for
  resolving scoped services from singletons.

---

## Hotfix 4: CostComparisonDto Dapper Materialization

### Symptom

Calling `GET /api/comparison/generate` returns `500 Internal Server Error`:

```json
{
  "StatusCode": 500,
  "Message": "A parameterless default constructor or one matching signature
              (System.Decimal monthorder, ...) is required for
              CostComparisonDto materialization"
}
```

### Root Cause

The `CostComparisonDto` was defined as a C# **positional record**:

```csharp
// BEFORE — BROKEN
internal sealed record CostComparisonDto(
    decimal MonthOrder,
    string MonthName,
    decimal TotalLeasePayments,
    decimal TotalPurchasePayments,
    decimal LeaseVsBuyDifference);
```

Dapper uses `Activator.CreateInstance` to materialize query results. It
looks for:

1. A **parameterless constructor** (to set properties via reflection), or
2. A **constructor with matching parameter names** (case-insensitive)

C# positional records generate a primary constructor with
`[CompilerGenerated]` attributes and specific IL naming. Dapper's constructor
resolution does not reliably match these generated constructors, especially
when:

- The SQL aliases return lowercase column names (`monthorder`)
- The record uses PascalCase parameter names (`MonthOrder`)
- The record is `internal sealed` (Dapper needs reflection access)

### Fix

**File:** `Src/LeaseBuyArch/Comparison/GenerateCostComparisonReport/Dtos/CostComparisonDto.cs`

Convert the record to a plain class with `{ get; set; }` properties:

```csharp
// AFTER — FIXED
internal sealed class CostComparisonDto
{
    public decimal MonthOrder { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal TotalLeasePayments { get; set; }
    public decimal TotalPurchasePayments { get; set; }
    public decimal LeaseVsBuyDifference { get; set; }
}
```

### Why This Fix Works

A plain class with a **compiler-generated parameterless constructor** and
public setters is the standard Dapper materialization target:

1. Dapper creates an instance via the parameterless constructor
2. Dapper uses `TypeDescriptor` or reflection to set properties by name
3. Property name matching is case-insensitive (`monthorder` → `MonthOrder`)
4. No special constructor resolution is needed

### Why Not Use Dapper's Custom Type Map?

Dapper supports `SqlMapper.ITypeMap` for custom materialization:

```csharp
SqlMapper.SetTypeMap(typeof(CostComparisonDto),
    new CustomPropertyTypeMap(typeof(CostComparisonDto),
        (type, columnName) => type.GetProperty(columnName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)));
```

This was rejected because:

- It requires global registration (runs once, affects all queries)
- It's fragile (property renames don't fail at compile time)
- A plain class is simpler, more idiomatic, and more maintainable

### SQL Columns vs Property Names

The Dapper query uses these column aliases:

```sql
EXTRACT(MONTH FROM l."PreparedAt") AS MonthOrder       -- decimal
TO_CHAR(l."PreparedAt", 'Month') AS MonthName           -- string
SUM(l."MonthlyPayment") AS TotalLeasePayments            -- decimal
p.TotalPurchasePayments AS TotalPurchasePayments         -- decimal
SUM(...) - COALESCE(...) AS LeaseVsBuyDifference          -- decimal
```

Dapper matches these to `CostComparisonDto` properties case-insensitively,
so `MonthOrder` (SQL) → `MonthOrder` (C#) works correctly.

---

## Summary of Changes

| # | File | Problem | Fix |
|---|---|---|---|
| 1 | `requests.http` | Wrong port (`:5000`) | Changed to `:5013` (matches `launchSettings.json`) |
| 2 | `appsettings.Development.json` | PostgreSQL host `postgres` not resolvable locally | Changed to `localhost` |
| 3 | `InMemoryEventBus.cs` | Singleton bus captured scoped `IMediator` → 500 on event publish | Use `IServiceScopeFactory` to create scope per publish |
| 4 | `CostComparisonDto.cs` | C# positional record not materializable by Dapper | Converted to plain class with `{ get; set; }` |

### Change Summary by Layer

```
requests.http              Configuration fix       (base URL port)
appsettings.Development.json  Configuration fix     (PostgreSQL hostname)
Common/Events/EventBus/    DI lifetime bugfix       (singleton→scope factory)
Comparison/.../Dtos/       Dapper compat fix        (record→class)
```

### Files NOT Changed

The following were considered but determined not to need changes:

| Consideration | Decision |
|---|---|
| `Directory.Build.props` `TreatWarningsAsErrors` | Already correct, no NU1510 warnings in this chapter |
| NuGet package versions | All packages are .NET 10-compatible (EF Core 10.0.8, Npgsql 10.0.1) |
| `Program.cs` registration order | Event bus registered before modules — correct |
| `SignLeaseEndpoint.cs` `MapPatch` | Correct verb (see Hotfix 3 — error was DI, not routing) |
| `Dockerfile` build context | Not used — we run locally, not in Docker |
