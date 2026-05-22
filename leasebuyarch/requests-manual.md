# requests.http User Manual

This guide walks through testing all API endpoints using the `requests.http` file.
It covers setup, step-by-step workflows, expected responses, and manual `curl` alternatives.

---

## Prerequisites

- **VS Code** with the **REST Client** extension (by Huachao Mao)
- OR **curl** available in your terminal
- The app must be running (see [Starting the App](#starting-the-app))

---

## Starting the App

### Option A: Docker (full stack)

```bash
cd leasebuyarch
make docker-up
```

PostgreSQL starts inside Docker. The app runs on port **8080** inside the container.
Update `@baseUrl` in `requests.http` to `http://localhost:8080`.

### Option B: Local dev (recommended for testing)

```bash
# 1. Start PostgreSQL
cd leasebuyarch/Src
docker compose up -d postgres

# 2. Build and run the app
export PATH="/usr/local/share/dotnet:$PATH"
cd ../../
make run
```

The app runs on `http://localhost:5013` (as configured in `launchSettings.json`).
The `@baseUrl` variable in `requests.http` is already set to this.

### Stopping the App

```bash
pkill -f "LeaseBuyArch"       # kills local dev process
make docker-down               # stops Docker containers
```

---

## How requests.http Works

The file contains REST Client format requests. Each block has:

```
### Comment describing the request
VERB /path
Header: value

body
```

You can either:

1. **VS Code:** Open `requests.http` and click **Send Request** above each block
2. **curl:** Copy the curl commands from the sections below

---

## End-to-End Workflows

### Workflow 1: Lease a Vehicle (prepare → sign)

#### Step 1 — Prepare Lease

**In VS Code:** Click "Send Request" above `### 1. Prepare a new lease`.

**With curl:**

```bash
curl -s -X POST http://localhost:5013/api/leasing \
  -H "Content-Type: application/json" \
  -d '{
    "customerId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "vehicleId": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
    "vehicleMsrp": 45000.00,
    "residualPercentage": 55,
    "moneyFactor": 0.00125,
    "termMonths": 36,
    "annualMileageLimit": 12000,
    "creditScore": 750
  }'
```

**Expected response:** `201 Created`
```json
"a1b2c3d4-..."
```

The response body is a UUID string — the new lease ID.

**What happens:**
- Validates credit score (≥ 700), mileage (≤ 15,000), term (≤ 36 months), previous lease settled
- Calculates monthly payment: `$649.69` for this example
- Persists lease to `Leasing.Leases` table in PostgreSQL
- Returns the lease ID

**If credit score is too low:** `409 Conflict`
```json
{"statusCode":409,"message":"Customer credit score 650 is below the minimum requirement of 700"}
```

#### Step 2 — Sign the Lease

Copy the lease ID from Step 1 into `@leaseId` at the top of `requests.http`.

**In VS Code:** Update `@leaseId`, then click "Send Request" above `### 2. Sign a prepared lease`.

**With curl:**

```bash
curl -s -i -X PATCH http://localhost:5013/api/leasing/a1b2c3d4-... \
  -H "Content-Type: application/json" \
  -d '{}'
```

**Expected response:** `204 No Content` (empty body)

**What happens:**
- Validates the lease exists (404 if not)
- Validates the lease is signed within 14 days of preparation (409 if too late)
- Sets `SignedAt` timestamp
- Publishes `LeaseSignedEvent` via the event bus
- The `LeaseSignedEventHandler` (in Vehicles module) marks the vehicle as "Leased"

#### Step 3 — Test Error: Sign Non-Existent Lease

**In VS Code:** Click "Send Request" above `### 4. Try signing a non-existent lease`.

**Expected response:** `404 Not Found`

---

### Workflow 2: Purchase a Vehicle (offer → complete)

#### Step 4 — Offer a Purchase

**In VS Code:** Click "Send Request" above `### 5. Offer a purchase`.

**With curl:**

```bash
curl -s -X POST http://localhost:5013/api/purchasing/offer \
  -H "Content-Type: application/json" \
  -d '{
    "customerId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    "vehicleId": "b2c3d4e5-f6a7-8901-bcde-f12345678901",
    "vehicleMsrp": 45000.00,
    "downPayment": 5000.00,
    "apr": 4.5,
    "termMonths": 60,
    "preparedAt": "2026-05-15T12:00:00Z"
  }'
```

**Expected response:** `201 Created`
```json
"<purchase-uuid>"
```

**What happens:**
- Validates the offer request
- Calculates monthly payment using the amortization formula: approximately `$745.68` for this example
- Persists purchase to `Purchasing.Purchases` table
- Returns the purchase ID

#### Step 5 — Complete the Purchase

Copy the purchase ID from Step 4 into `@purchaseId` at the top of `requests.http`.

**In VS Code:** Update `@purchaseId`, then click "Send Request" above `### 6. Complete a purchase`.

**With curl:**

```bash
curl -s -i -X PATCH http://localhost:5013/api/purchasing/a1b2c3d4-... \
  -H "Content-Type: application/json" \
  -d '{}'
```

**Expected response:** `204 No Content`

**What happens:**
- Marks the purchase loan as fully paid off
- Sets `CompletedAt` timestamp
- Publishes `PurchaseCompletedEvent`
- The `PurchaseCompletedEventHandler` (in Vehicles module) marks the vehicle as "Owned"

#### Step 6 — List All Purchases

**In VS Code:** Click "Send Request" above `### 7. Get all purchases`.

**With curl:**

```bash
curl -s http://localhost:5013/api/purchasing | json_pp
```

**Expected response:** `200 OK`
```json
[
  {
    "id": "<purchase-uuid>",
    "customerId": "a1b2c3d4-...",
    "vehicleId": "b2c3d4e5-...",
    "vehicleMsrp": 45000.00,
    "monthlyPayment": 745.68,
    "termMonths": 60,
    "preparedAt": "2026-05-15T12:00:00Z",
    "completedAt": "2026-05-17T..."
  }
]
```

If no purchases exist, returns an empty array `[]`.

---

### Workflow 3: Compare Lease vs Buy

#### Step 7 — Generate Comparison Report

> **Note:** This endpoint reads from the database. For meaningful results, run at least one lease and one purchase first (Workflows 1-2).

**In VS Code:** Click "Send Request" above `### 8. Generate lease vs buy cost comparison report`.

**With curl:**

```bash
curl -s http://localhost:5013/api/comparison/generate | json_pp
```

**Expected response:** `200 OK`
```json
{
  "reportData": [ ... ]
}
```

The report compares leasing vs purchasing costs for all vehicles in the current year.

---

## Error Handling Reference

| Scenario | HTTP Status | Error Message |
|---|---|---|
| Credit score < 700 | `409 Conflict` | "Customer credit score {score} is below the minimum requirement of 700" |
| Annual mileage > 15,000 | `409 Conflict` | "Annual mileage limit {limit} exceeds maximum allowed 15000" |
| Lease term > 36 months | `409 Conflict` | "Lease term {term} months exceeds maximum 36 months" |
| Previous lease not settled | `409 Conflict` | "Previous lease must be settled by the customer" |
| Signing after 14 days | `409 Conflict` | "Lease can not be signed because more than 14 days have passed from the lease preparation" |
| Lease/purchase not found | `404 Not Found` | (empty body) |
| Low credit score test | `409 Conflict` | (see above) |
| Internal server error | `500 Internal Server Error` | (exception details in development mode) |

---

## Quick Reference: All Endpoints

| # | Method | URL | Purpose | Body |
|---|---|---|---|---|
| 1 | POST | `/api/leasing` | Prepare a lease | `PrepareLeaseRequest` |
| 2 | PATCH | `/api/leasing/{id}` | Sign a lease | `{}` |
| 3 | POST | `/api/leasing` | Negative test: low credit | `PrepareLeaseRequest` (score=650) |
| 4 | PATCH | `/api/leasing/{id}` | Negative test: not found | `{}` |
| 5 | POST | `/api/purchasing/offer` | Offer a purchase | `OfferPurchaseRequest` |
| 6 | PATCH | `/api/purchasing/{id}` | Complete a purchase | `{}` |
| 7 | GET | `/api/purchasing` | List all purchases | — |
| 8 | GET | `/api/comparison/generate` | Lease vs buy report | — |

---

## Manual curl Test (One-Liner)

Run the full happy path in one go:

```bash
# Prepare a lease
LEASE_ID=$(curl -s -X POST http://localhost:5013/api/leasing \
  -H "Content-Type: application/json" \
  -d '{"customerId":"a1b2c3d4-e5f6-7890-abcd-ef1234567890","vehicleId":"b2c3d4e5-f6a7-8901-bcde-f12345678901","vehicleMsrp":45000,"residualPercentage":55,"moneyFactor":0.00125,"termMonths":36,"annualMileageLimit":12000,"creditScore":750}' | tr -d '"')
echo "Lease ID: $LEASE_ID"

# Sign it
curl -s -o /dev/null -w "Sign status: %{http_code}\n" -X PATCH "http://localhost:5013/api/leasing/$LEASE_ID" \
  -H "Content-Type: application/json" -d '{}'
```

## Troubleshooting

| Problem | Likely Cause | Fix |
|---|---|---|
| `Connection refused` | App not running | Run `make run` first |
| `404 Not Found` on PATCH | Wrong lease/purchase ID | Copy the ID from the POST response |
| `405 Method Not Allowed` on PATCH | Empty/invalid ID in URL | Verify `@leaseId` / `@purchaseId` is a valid UUID |
| `500 Internal Server Error` | Event bus scoping issue | Restart the app after code changes |
| `Sequence contains more than one element` | Same customer has multiple leases | Use a fresh `customerId` (run `uuidgen`) |
| `nodename nor servname provided` | PostgreSQL hostname wrong | Use `localhost` not `postgres` in connection string |

---

## Architecture Notes

The application has four bounded contexts, each with its own database schema:

```
Context       Schema        Tables
───────       ──────        ──────
Leasing       Leasing       Leases
Purchasing    Purchasing    Purchases
Vehicles      Vehicles      Vehicles
Comparison    (Dapper queries across all schemas)
```

All data is in the same PostgreSQL database (`leasebuy`), separated by schema.
When a lease is signed or a purchase is completed, events flow through the in-memory event bus:

```
Prepare Lease  →  POST /api/leasing      →  Lease persisted
Sign Lease     →  PATCH /api/leasing/{id} →  LeaseSignedEvent published
                                              └→ Vehicle marked as "Leased"
Offer Purchase →  POST /api/purchasing/offer → Purchase persisted
Complete Purchase → PATCH /api/purchasing/{id} → PurchaseCompletedEvent published
                                                   └→ Vehicle marked as "Owned"
```
