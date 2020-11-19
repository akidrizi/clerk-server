FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ClerkServer.UnitTests/ClerkServer.UnitTests.csproj", "ClerkServer.UnitTests/"]
COPY ["ClerkServer/ClerkServer.csproj", "ClerkServer/"]
RUN dotnet restore "ClerkServer.UnitTests/ClerkServer.UnitTests.csproj"
COPY . .
WORKDIR "/src/ClerkServer.UnitTests"
RUN dotnet build "ClerkServer.UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClerkServer.UnitTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClerkServer.UnitTests.dll"]