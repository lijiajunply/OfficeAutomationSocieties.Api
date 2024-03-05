﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OfficeAutomationSocieties.Api/OfficeAutomationSocieties.Api.csproj", "OfficeAutomationSocieties.Api/"]
RUN dotnet restore "OfficeAutomationSocieties.Api/OfficeAutomationSocieties.Api.csproj"
COPY . .
WORKDIR "/src/OfficeAutomationSocieties.Api"
RUN dotnet build "OfficeAutomationSocieties.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OfficeAutomationSocieties.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficeAutomationSocieties.Api.dll"]
