using System.Reflection;
using EvolutionaryArchitecture.LeaseBuyArch;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Api;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Api;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSystemClock();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(
        typeof(EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease.PrepareLeaseCommand).Assembly,
        typeof(EvolutionaryArchitecture.LeaseBuyArch.Vehicles.DataAccess.Vehicle).Assembly);
});
builder.Services.AddCommonInfrastructure();

builder.Services.AddLeasing(builder.Configuration, EvolutionaryArchitecture.LeaseBuyArch.Module.Leasing);
builder.Services.AddPurchasing(builder.Configuration, EvolutionaryArchitecture.LeaseBuyArch.Module.Purchasing);
builder.Services.AddVehicles(builder.Configuration, EvolutionaryArchitecture.LeaseBuyArch.Module.Vehicles);
builder.Services.AddComparison(builder.Configuration, EvolutionaryArchitecture.LeaseBuyArch.Module.Comparison);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();
app.MapControllers();

app.RegisterLeasing(EvolutionaryArchitecture.LeaseBuyArch.Module.Leasing);
app.RegisterPurchasing(EvolutionaryArchitecture.LeaseBuyArch.Module.Purchasing);
app.RegisterVehicles(EvolutionaryArchitecture.LeaseBuyArch.Module.Vehicles);
app.RegisterComparison(EvolutionaryArchitecture.LeaseBuyArch.Module.Comparison);

app.Run();

namespace EvolutionaryArchitecture.LeaseBuyArch
{
    [UsedImplicitly]
    public sealed class Program { }
}
