FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ClerkServer/ClerkServer.csproj", "ClerkServer/"]
COPY ["ClerkServer.UnitTests/ClerkServer.UnitTests.csproj", "ClerkServer.UnitTests/"]
RUN dotnet restore "ClerkServer/ClerkServer.csproj"
RUN dotnet restore "ClerkServer.UnitTests/ClerkServer.UnitTests.csproj"
COPY . .
WORKDIR "/src/ClerkServer.UnitTests"
RUN dotnet build "ClerkServer/ClerkServer.csproj" -c Release -o /app/build
RUN dotnet build "ClerkServer.UnitTests/ClerkServer.UnitTests.csproj" -c Release -o /app/build

RUN dotnet test "ClerkServer.UnitTests/ClerkServer.UnitTests.csproj" --logger "trx;LogFileName=clerk-server-tests.trx"

FROM build AS publish
RUN dotnet publish "ClerkServer.UnitTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClerkServer.UnitTests.dll"]