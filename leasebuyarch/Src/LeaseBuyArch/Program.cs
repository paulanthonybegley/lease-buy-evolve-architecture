using EvolutionaryArchitecture.LeaseBuyArch.Common.ErrorHandling;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Events.EventBus;
using EvolutionaryArchitecture.LeaseBuyArch.Common.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Validation.Requests;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing;
using EvolutionaryArchitecture.LeaseBuyArch.Purchasing;
using EvolutionaryArchitecture.LeaseBuyArch.Vehicles;
using EvolutionaryArchitecture.LeaseBuyArch.Comparison;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSystemClock();
builder.Services.AddEventBus();
builder.Services.AddRequestsValidations();

builder.Services.AddLeasing(builder.Configuration);
builder.Services.AddPurchasing(builder.Configuration);
builder.Services.AddVehicles(builder.Configuration);
builder.Services.AddComparison();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseLeasing();
app.UsePurchasing();
app.UseVehicles();
app.UseComparison();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandling();

app.MapControllers();

app.MapLeasing();
app.MapPurchasing();
app.MapComparison();

app.Run();

namespace EvolutionaryArchitecture.LeaseBuyArch
{
    [UsedImplicitly]
    public sealed class Program
    {
    }
}
