FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Leasing/Directory.Build.props Leasing/
COPY LeaseBuyArch.Common/Directory.Build.props LeaseBuyArch.Common/
COPY ["Leasing/LeaseBuyArch.Leasing/LeaseBuyArch.Leasing.csproj", "Leasing/LeaseBuyArch.Leasing/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Core/LeaseBuyArch.Common.Core.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Core/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Api/LeaseBuyArch.Common.Api.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Api/"]
COPY ["LeaseBuyArch.Common/LeaseBuyArch.Common.Infrastructure/LeaseBuyArch.Common.Infrastructure.csproj", "LeaseBuyArch.Common/LeaseBuyArch.Common.Infrastructure/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Core/LeaseBuyArch.Leasing.Core.csproj", "Leasing/LeaseBuyArch.Leasing.Core/"]
COPY ["Leasing/LeaseBuyArch.Leasing.IntegrationEvents/LeaseBuyArch.Leasing.IntegrationEvents.csproj", "Leasing/LeaseBuyArch.Leasing.IntegrationEvents/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Application/LeaseBuyArch.Leasing.Application.csproj", "Leasing/LeaseBuyArch.Leasing.Application/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Infrastructure/LeaseBuyArch.Leasing.Infrastructure.csproj", "Leasing/LeaseBuyArch.Leasing.Infrastructure/"]
COPY ["Leasing/LeaseBuyArch.Leasing.Api/LeaseBuyArch.Leasing.Api.csproj", "Leasing/LeaseBuyArch.Leasing.Api/"]
RUN dotnet restore "Leasing/LeaseBuyArch.Leasing/LeaseBuyArch.Leasing.csproj"
COPY . .
WORKDIR "/src/Leasing/LeaseBuyArch.Leasing"
RUN dotnet build "LeaseBuyArch.Leasing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeaseBuyArch.Leasing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvolutionaryArchitecture.LeaseBuyArch.Leasing.dll"]
