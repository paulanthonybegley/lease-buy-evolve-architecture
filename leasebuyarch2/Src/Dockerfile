FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Directory.Build.props ./
COPY ["LeaseBuyArch/LeaseBuyArch.csproj", "LeaseBuyArch/"]
COPY ["Common/LeaseBuyArch.Common.Core/LeaseBuyArch.Common.Core.csproj", "Common/LeaseBuyArch.Common.Core/"]
COPY ["Common/LeaseBuyArch.Common.Api/LeaseBuyArch.Common.Api.csproj", "Common/LeaseBuyArch.Common.Api/"]
COPY ["Common/LeaseBuyArch.Common.Infrastructure/LeaseBuyArch.Common.Infrastructure.csproj", "Common/LeaseBuyArch.Common.Infrastructure/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Core/LeaseBuyArch.Leasing.Core.csproj", "Leasing/LeaseBuyArch.Leasing.Core/"]
COPY ["Leasing/LeaseBuyArch.Leasing.IntegrationEvents/LeaseBuyArch.Leasing.IntegrationEvents.csproj", "Leasing/LeaseBuyArch.Leasing.IntegrationEvents/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Application/LeaseBuyArch.Leasing.Application.csproj", "Leasing/LeaseBuyArch.Leasing.Application/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Infrastructure/LeaseBuyArch.Leasing.Infrastructure.csproj", "Leasing/LeaseBuyArch.Leasing.Infrastructure/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Api/LeaseBuyArch.Leasing.Api.csproj", "Leasing/LeaseBuyArch.Leasing.Api/"]
COPY ["Purchasing/LeaseBuyArch.Purchasing.IntegrationEvents/LeaseBuyArch.Purchasing.IntegrationEvents.csproj", "Purchasing/LeaseBuyArch.Purchasing.IntegrationEvents/"]
COPY ["Purchasing/LeaseBuyArch.Purchasing.DataAccess/LeaseBuyArch.Purchasing.DataAccess.csproj", "Purchasing/LeaseBuyArch.Purchasing.DataAccess/"]
COPY ["Purchasing/LeaseBuyArch.Purchasing.Api/LeaseBuyArch.Purchasing.Api.csproj", "Purchasing/LeaseBuyArch.Purchasing.Api/"]
COPY ["Vehicles/LeaseBuyArch.Vehicles.DataAccess/LeaseBuyArch.Vehicles.DataAccess.csproj", "Vehicles/LeaseBuyArch.Vehicles.DataAccess/"]
COPY ["Vehicles/LeaseBuyArch.Vehicles.Api/LeaseBuyArch.Vehicles.Api.csproj", "Vehicles/LeaseBuyArch.Vehicles.Api/"]
COPY ["Comparison/LeaseBuyArch.Comparison/LeaseBuyArch.Comparison.csproj", "Comparison/LeaseBuyArch.Comparison/"]
RUN dotnet restore "LeaseBuyArch/LeaseBuyArch.csproj"
COPY . .
WORKDIR "/src/LeaseBuyArch"
RUN dotnet build "LeaseBuyArch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeaseBuyArch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvolutionaryArchitecture.LeaseBuyArch.dll"]
