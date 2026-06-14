Project Title
# Product Management API

Overview
RESTful Product Management API built using ASP.NET Core 8 following Clean Architecture principles.


Tech Stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger
- FluentValidation
- Serilog
- xUnit
- Moq
- Docker

Architecture
API
Application
Domain
Infrastructure

Features
- Product CRUD
- JWT Authentication
- Refresh Token
- API Versioning
- Logging
- Validation
- Exception Handling
- Unit Testing

Run
dotnet restore
dotnet build
dotnet ef database update --project src/Infrastructure --startup-project src/API
dotnet run --project src/API


Swagger
https://localhost:7169/swagger