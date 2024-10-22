FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# copy csproj and restore as distinct layers *.csproj ./
COPY ["EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj", "EvacuationPlanningMonitoring/"]
RUN dotnet restore "EvacuationPlanningMonitoring/EvacuationPlanningMonitoring.csproj"
# copy everything else and build
COPY . .
WORKDIR "/src/EvacuationPlanningMonitoring"
RUN dotnet build "EvacuationPlanningMonitoring.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EvacuationPlanningMonitoring.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvacuationPlanningMonitoring.dll"]