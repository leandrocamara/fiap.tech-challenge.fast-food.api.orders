# API Fast Food

_API Fast Food_ is .net project for tech changeled.

## Application Tech Stack

- .NET 8.0
- ASP.NET Core (v8.0)
- Entity Framework Core
- Fluent Validation
- ASP.NET Core Identity + JWT Token
- SQL Server
- AutoMapper
- Swashbuckle (Swagger Open API)
- xUnit + Moq
- EF Core Migrations

## Test Coverage

## Run the Application

Navigate to the root of the project. For building the project using command line, run below command :

`dotnet build`

Run the service in the command line. Navigate to the bin/Debug/net8.0 directory.

`dotnet run --project APIFastFood`

## Docker build and run

`docker build --tag=apifastfood:1.0 .`

`docker run -p 8080:8080 apifastfood:1.0`

## API Doc

Api Doc(Swagger) will be served on following path;

http://localhost:8080/swagger
