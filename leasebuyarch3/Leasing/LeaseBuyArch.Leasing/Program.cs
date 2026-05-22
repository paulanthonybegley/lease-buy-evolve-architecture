using EvolutionaryArchitecture.LeaseBuyArch.Common.Api.ErrorHandling;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Core.SystemClock;
using EvolutionaryArchitecture.LeaseBuyArch.Common.Infrastructure;
using EvolutionaryArchitecture.LeaseBuyArch.Leasing.Api;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSystemClock();
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(
        typeof(EvolutionaryArchitecture.LeaseBuyArch.Leasing.Application.PrepareLease.PrepareLeaseCommand).Assembly));
builder.Services.AddLeasing(builder.Configuration);
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
app.RegisterLeasing();

app.Run();

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing
{
    [JetBrains.Annotations.UsedImplicitly]
    public sealed class Program { }
}
