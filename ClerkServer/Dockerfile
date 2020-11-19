FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ClerkServer.csproj", "ClerkServer/"]
RUN dotnet restore "ClerkServer/ClerkServer.csproj"
COPY . .
WORKDIR "/src/ClerkServer"
RUN dotnet build "ClerkServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClerkServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClerkServer.dll"]