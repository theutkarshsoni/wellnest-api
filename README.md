# WellNest API

A minimal, modular **.NET 8** Web API for tracking wellness habits (backend-first focus).
Built to demonstrate **C#/.NET**, **SQL Server**, **Azure App Service**, and **Azure DevOps CI/CD**.

## Tech Stack
- **Backend:** .NET 8 Minimal API, C#
- **Data:** (Phase 2) In-memory → (Phase 3) SQL Server with EF Core
- **Cloud/DevOps:** Azure App Service, Azure SQL, Azure DevOps Pipelines
- **Testing:** xUnit (planned)

## Timeline
- Phase 1: project scaffold, healthcheck
- Phase 2: modular endpoints + in-memory repository for `/habits`
- Phase 3: SQL Server + EF Core + migrations
- Phase 4: Azure deployment
- Phase 5: Azure DevOps CI/CD

### Prerequisites
- .NET SDK 8.x
- SQL Server (Docker/local/Azure SQL)

### Run locally
```bash
dotnet restore
dotnet run --project WellNest.Api
