FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CalorieTracker/CalorieTracker.csproj", "CalorieTracker/"]
RUN dotnet restore "CalorieTracker/CalorieTracker.csproj"
COPY . .
WORKDIR "/src/CalorieTracker"
RUN dotnet build "CalorieTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalorieTracker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalorieTracker.dll"]
