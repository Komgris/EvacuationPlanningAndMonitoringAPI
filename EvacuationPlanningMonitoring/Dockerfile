FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

ARG Configuration=Release
EXPOSE 80
EXPOSE 443

COPY . ./

RUN dotnet restore ./EvacuationPlanningMonitoring.csproj

RUN dotnet build ./EvacuationPlanningMonitoring.csproj -c ${Configuration} --no-restore

RUN dotnet publish ./EvacuationPlanningMonitoring.csproj -c ${Configuration} -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
RUN ls -la

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EvacuationPlanningMonitoring.dll"]
