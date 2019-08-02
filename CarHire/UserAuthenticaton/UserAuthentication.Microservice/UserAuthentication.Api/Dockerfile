FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["UserAuthentication.Api/UserAuthentication.Api.csproj", "UserAuthentication.Api/"]
COPY ["UserAuthentication.Domain/UserAuthentication.Domain.csproj", "UserAuthentication.Domain/"]
COPY ["UserAuthentication.Infrastructure/UserAuthentication.Infrastructure.csproj", "UserAuthentication.Infrastructure/"]
COPY ["UserAuthentication.Application/UserAuthentication.Application.csproj", "UserAuthentication.Application/"]
RUN dotnet restore "UserAuthentication.Api/UserAuthentication.Api.csproj"
COPY . .
WORKDIR "/src/UserAuthentication.Api"
RUN dotnet build "UserAuthentication.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserAuthentication.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserAuthentication.Api.dll"]
