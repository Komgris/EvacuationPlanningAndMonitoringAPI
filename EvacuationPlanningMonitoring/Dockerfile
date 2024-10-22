FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

ARG Configuration=Release
EXPOSE 80
EXPOSE 443
COPY . .
RUN dotnet restore

RUN dotnet build -c ${Configuration}

RUN dotnet publish -c ${Configuration} -o publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
RUN ls -la

COPY --from=build /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "EvacuationPlanningMonitoring.dll"]