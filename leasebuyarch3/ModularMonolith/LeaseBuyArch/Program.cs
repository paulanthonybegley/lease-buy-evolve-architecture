using EvolutionaryArchitecture.LeaseBuyArch;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSystemClock();
builder.Services.AddFeatureManagement();

builder.Services.AddPurchasing(builder.Configuration, Module.Purchasing);
builder.Services.AddVehicles(builder.Configuration, Module.Vehicles);
builder.Services.AddComparison(builder.Configuration, Module.Comparison);
builder.Services.AddCommonInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();

app.RegisterPurchasing(Module.Purchasing);
app.RegisterVehicles(Module.Vehicles);
app.RegisterComparison(Module.Comparison);

app.Run();

namespace EvolutionaryArchitecture.LeaseBuyArch
{
    [JetBrains.Annotations.UsedImplicitly]
    public sealed class Program { }
}
