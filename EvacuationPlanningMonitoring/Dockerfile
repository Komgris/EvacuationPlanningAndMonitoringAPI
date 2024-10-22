FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

ARG Configuration=Release
EXPOSE 80
EXPOSE 443

COPY *.sln .
COPY EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj ./EvacuationPlanningMonitoring/

RUN dotnet restore ./EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj

COPY EvacuationPlanningMonitoring/. ./EvacuationPlanningMonitoring/

RUN dotnet build ./EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj -c ${Configuration} --no-restore

RUN dotnet publish ./EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj -c ${Configuration} -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
RUN ls -la

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EvacuationPlanningMonitoring.dll"]
