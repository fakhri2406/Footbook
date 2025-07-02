## Footbook (in development, will be available soon)

Welcome to **Footbook**, a social sports platform designed to help football lovers in **Baku** organize and participate in local matches with ease. By connecting players, teams, and Azfar Stadium branches in a seamless experience, Footbook brings amateur football to a new level of organization and accessibility.

Baku has a vibrant amateur football scene — but organizing matches is often difficult and chaotic. **Footbook** bridges that gap by digitizing the experience

---

### 🔑 Key Features

- **Branch & Match Scheduling:** Easily browse and book fields at Azfar Stadium locations nearby.
- **Team Management:** Create teams and join open game slots.
- **Slot Feed:** Stay updated with upcoming slots and their availability.
- **Notifications & Reminders:** Get alerts for match invites, confirmations, and cancellations.
- **Player Profiles (coming soon):** Track attendance, past performance, and your personal football history.

---

### 🏗️ Project Architecture

Footbook follows a **Clean, N-Layered Architecture** powered by **ASP.NET Core**, promoting separation of concerns and scalability.

```
Footbook/
├── Footbook.API/               # REST endpoints, controllers, middlewares
├── Footbook.Infrastructure/    # Business logic, services, validators, helpers, etc. 
├── Novademy.Data/              # EF Core, repositories, domain models
└── Novademy.Core/              # DTOs, Enums
```

Each layer is decoupled, promoting testability and flexibility as the application scales.

---

### 🔧 Tech Stack

#### **Backend**
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI
- xUnit (planned)
- Moq (planned)

#### **Database (Planned, currently local)**
- MS Azure SQL Server

#### **Hosting (Planned)**
- MS Azure App Service or any compatible cloud host

---

### 📌 Status

> Currently in **active development**. Core domain models, infrastructure services, and initial endpoints are being implemented. Upcoming milestones include game history flows and mobile interface.
