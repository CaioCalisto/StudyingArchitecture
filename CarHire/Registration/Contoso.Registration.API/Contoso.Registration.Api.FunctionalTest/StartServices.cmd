dotnet publish ..\..\..\..\Contoso.Registration.API
setx ASPNETCORE_ENVIRONMENT "dev"
dotnet ..\..\..\..\Contoso.Registration.API\bin\Debug\netcoreapp3.1\publish\Contoso.Registration.Api.dll