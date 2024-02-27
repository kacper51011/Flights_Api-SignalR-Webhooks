#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Flights/Flights.Api/Flights.Api.csproj", "Flights/Flights.Api/"]
COPY ["Flights/Flights.Application/Flights.Application.csproj", "Flights/Flights.Application/"]
COPY ["Flights/Flights.Domain/Flights.Domain.csproj", "Flights/Flights.Domain/"]
COPY ["Flights/Flights.Infrastructure/Flights.Infrastructure.csproj", "Flights/Flights.Infrastructure/"]
RUN dotnet restore "./Flights/Flights.Api/./Flights.Api.csproj"
COPY . .
WORKDIR "/src/Flights/Flights.Api"
RUN dotnet build "./Flights.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Flights.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flights.Api.dll"]