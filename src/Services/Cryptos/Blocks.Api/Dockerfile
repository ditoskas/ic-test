# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cryptos/Blocks.Api/Blocks.Api.csproj", "Services/Cryptos/Blocks.Api/"]
COPY ["Core/IcTest.Infrastructure/IcTest.Infrastructure.csproj", "Core/IcTest.Infrastructure/"]
COPY ["Common/BlockCypher/BlockCypher.Client/BlockCypher.Client.csproj", "Common/BlockCypher/BlockCypher.Client/"]
COPY ["Common/BlockCypher/BlockCypher.Data/BlockCypher.Data.csproj", "Common/BlockCypher/BlockCypher.Data/"]
COPY ["Core/IcTest.Data/IcTest.Data.csproj", "Core/IcTest.Data/"]
COPY ["Common/IcTest.Shared/IcTest.Shared.csproj", "Common/IcTest.Shared/"]
COPY ["Modules/BlockTransactionsModule/BlockTransactionsModule.csproj", "Modules/BlockTransactionsModule/"]
RUN dotnet restore "./Services/Cryptos/Blocks.Api/Blocks.Api.csproj"
COPY . .
WORKDIR "/src/Services/Cryptos/Blocks.Api"
RUN dotnet build "./Blocks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blocks.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blocks.Api.dll"]