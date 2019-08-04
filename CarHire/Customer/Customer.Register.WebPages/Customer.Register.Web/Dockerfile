FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Customer.Register.Web/Customer.Register.Web.csproj", "Customer.Register.Web/"]
COPY ["Customer.Register.Application/Customer.Register.Application.csproj", "Customer.Register.Application/"]
RUN dotnet restore "Customer.Register.Web/Customer.Register.Web.csproj"
COPY . .
WORKDIR "/src/Customer.Register.Web"
RUN dotnet build "Customer.Register.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Customer.Register.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Customer.Register.Web.dll"]
