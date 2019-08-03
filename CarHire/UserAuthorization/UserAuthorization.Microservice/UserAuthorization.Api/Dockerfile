FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["UserAuthorization.Api/UserAuthorization.Api.csproj", "UserAuthorization.Api/"]
COPY ["UserAuthorization.Domain/UserAuthorization.Domain.csproj", "UserAuthorization.Domain/"]
COPY ["UserAuthorization.Infrastructure/UserAuthorization.Infrastructure.csproj", "UserAuthorization.Infrastructure/"]
COPY ["UserAuthorization.Application/UserAuthorization.Application.csproj", "UserAuthorization.Application/"]
RUN dotnet restore "UserAuthorization.Api/UserAuthorization.Api.csproj"
COPY . .
WORKDIR "/src/UserAuthorization.Api"
RUN dotnet build "UserAuthorization.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserAuthorization.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserAuthorization.Api.dll"]
