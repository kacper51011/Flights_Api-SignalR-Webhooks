#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8001
EXPOSE 8005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlightsExternalConsumer/FlightsConsumer.Api/FlightsConsumer.Api.csproj", "FlightsExternalConsumer/FlightsConsumer.Api/"]
RUN dotnet restore "./FlightsExternalConsumer/FlightsConsumer.Api/./FlightsConsumer.Api.csproj"
COPY . .
WORKDIR "/src/FlightsExternalConsumer/FlightsConsumer.Api"
RUN dotnet build "./FlightsConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlightsConsumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightsConsumer.Api.dll"]