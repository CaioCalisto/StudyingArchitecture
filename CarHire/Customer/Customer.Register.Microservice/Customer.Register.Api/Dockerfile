FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Customer.Register.Api/Customer.Register.Api.csproj Customer.Register.Api/
COPY Customer.Register.Application/Customer.Register.Application.csproj Customer.Register.Application/
COPY Customer.Register.Infrastructure/Customer.Register.Infrastructure.csproj Customer.Register.Infrastructure/
COPY Customer.Register.Domain/Customer.Register.Domain.csproj Customer.Register.Domain/
RUN dotnet restore Customer.Register.Api/Customer.Register.Api.csproj
COPY . .
WORKDIR /src/Customer.Register.Api
RUN dotnet build Customer.Register.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Customer.Register.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Customer.Register.Api.dll"]
