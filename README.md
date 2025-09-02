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

## Request Flow: .NET Minimal API vs Node.js/Express

```mermaid
flowchart TD
    A[Client Request] --> B[Routing / Endpoint]
    B --> C[DTO (Request/Response)]
    C --> D[Domain Model (Business Rules)]
    D --> E[Repository (Abstraction)]
    E --> F[Database]
    F --> G[Response to Client]

    %% .NET notes
    B --- B1[.NET: app.MapGet/MapPost]
    C --- C1[.NET: Auto-bind DTO]
    D --- D1[.NET: Habit.cs methods]
    E --- E1[.NET: IHabitRepository]
    F --- F1[SQL Server via EF Core]

    %% Node.js notes
    B --- B2[Node: router.get/post]
    C --- C2[Node: req.body + Joi/Zod]
    D --- D2[Node: Mongoose schema methods]
    E --- E2[Node: Direct Mongoose calls]
    F --- F2[MongoDB]


## Design Choices & Learnings

- **In-Memory First:** Started with an in-memory repository before SQL Server. This allowed me to validate routes, DTOs, and business rules quickly, without the overhead of database setup. Later I can swap the implementation to SQL Server by keeping the same `IHabitRepository` contract.
- **Repository Pattern:** By coding against an interface (`IHabitRepository`), my endpoints don’t depend on *where* the data comes from. This separation makes testing easier and storage replaceable.
- **DTOs for Contracts:** Used DTOs (`CreateHabitRequest`, `HabitResponse`) to separate internal models from external API contracts. This prevents leaking internal details and keeps the API stable even if domain logic changes.
- **Minimal API + Modular Endpoints:** Organized routes into modular endpoint groups (`MapHabitEndpoints`) for scalability and maintainability, instead of dumping everything inside `Program.cs`.
- **Dependency Injection:** Registered repositories via DI (`builder.Services.AddSingleton`) so implementations can be swapped easily (in-memory now, SQL Server later).

This progression mirrors real-world enterprise practices: **prototype quickly → validate → add persistence → scale → productionize.**
