# Etap 1: Budowanie i testowanie
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

# Restore i testy
RUN dotnet restore

# Testy jednostkowe
RUN dotnet test src/Shared/JobOffersApi.Shared.Tests/JobOffersApi.Shared.Tests.csproj --no-restore --verbosity normal \
    && dotnet test src/Modules/Users/JobOffersApi.Modules.Users.Tests/JobOffersApi.Modules.Users.Tests.Unit.csproj --no-restore --verbosity normal \
    && dotnet test src/Modules/Companies/JobOffersApi.Modules.Companies.Tests/JobOffersApi.Modules.Companies.Tests.Unit.csproj --no-restore --verbosity normal \
    && dotnet test src/Modules/JobOffers/JobOffersApi.Modules.JobOffers.Tests/JobOffersApi.Modules.JobOffers.Tests.Unit.csproj --no-restore --verbosity normal

# Build i publish
RUN dotnet build src/Bootstrapper/JobOffersApi.Bootstrapper/JobOffersApi.Bootstrapper.csproj --no-restore -c Release \
    && dotnet publish src/Bootstrapper/JobOffersApi.Bootstrapper/JobOffersApi.Bootstrapper.csproj --no-restore -c Release -o /app/publish


# Etap 2: Obraz runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "JobOffersApi.Bootstrapper.dll"]
