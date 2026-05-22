FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ModularMonolith/Directory.Build.props ModularMonolith/
COPY LeaseBuyArch.Common/Directory.Build.props LeaseBuyArch.Common/
COPY Leasing/Directory.Build.props Leasing/
COPY ["ModularMonolith/LeaseBuyArch/LeaseBuyArch.csproj", "ModularMonolith/LeaseBuyArch/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Core/LeaseBuyArch.Common.Core.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Core/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Api/LeaseBuyArch.Common.Api.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Api/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Infrastructure/LeaseBuyArch.Common.Infrastructure.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Infrastructure/"]
COPY ["ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.IntegrationEvents/LeaseBuyArch.Purchasing.IntegrationEvents.csproj", "ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.IntegrationEvents/"]
COPY ["ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.DataAccess/LeaseBuyArch.Purchasing.DataAccess.csproj", "ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.DataAccess/"]
COPY ["ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.Api/LeaseBuyArch.Purchasing.Api.csproj", "ModularMonolith/Purchasing/LeaseBuyArch.Purchasing.Api/"]
COPY ["ModularMonolith/Vehicles/LeaseBuyArch.Vehicles.DataAccess/LeaseBuyArch.Vehicles.DataAccess.csproj", "ModularMonolith/Vehicles/LeaseBuyArch.Vehicles.DataAccess/"]
COPY ["ModularMonolith/Vehicles/LeaseBuyArch.Vehicles.Api/LeaseBuyArch.Vehicles.Api.csproj", "ModularMonolith/Vehicles/LeaseBuyArch.Vehicles.Api/"]
COPY ["ModularMonolith/Comparison/LeaseBuyArch.Comparison/LeaseBuyArch.Comparison.csproj", "ModularMonolith/Comparison/LeaseBuyArch.Comparison/"]
COPY ["Leasing/LeaseBuyArch.Leasing.IntegrationEvents/LeaseBuyArch.Leasing.IntegrationEvents.csproj", "Leasing/LeaseBuyArch.Leasing.IntegrationEvents/"]
RUN dotnet restore "ModularMonolith/LeaseBuyArch/LeaseBuyArch.csproj"
COPY . .
WORKDIR "/src/ModularMonolith/LeaseBuyArch"
RUN dotnet build "LeaseBuyArch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeaseBuyArch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvolutionaryArchitecture.LeaseBuyArch.dll"]
