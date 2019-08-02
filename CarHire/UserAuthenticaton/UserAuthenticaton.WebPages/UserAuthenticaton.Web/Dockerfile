FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["UserAuthenticaton.Web/UserAuthenticaton.Web.csproj", "UserAuthenticaton.Web/"]
RUN dotnet restore "UserAuthenticaton.Web/UserAuthenticaton.Web.csproj"
COPY . .
WORKDIR "/src/UserAuthenticaton.Web"
RUN dotnet build "UserAuthenticaton.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserAuthenticaton.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserAuthenticaton.Web.dll"]
