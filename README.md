# Lubricentro Vélez — Management System (v2)

Desktop application built for a real automotive lubrication shop (*lubricentro*)
to run its daily operations: customers and their vehicles, service work orders,
and the catalog of oils, additives and filters used on each job.

It is a **full rewrite** of a legacy in-house system. The application was rebuilt
from scratch on top of the shop's **existing SQL Server database**, fixing
long-standing bugs from the original version and completing features that were
never finished.

> This is production software delivered to a specific business, not a generic
> template. The repository is public so its scope and code can be reviewed as a
> portfolio project.

![.NET](https://img.shields.io/badge/.NET-6.0-512BD4)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6)
![EF Core](https://img.shields.io/badge/ORM-EF%20Core%206-512BD4)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927)
![C#](https://img.shields.io/badge/C%23-11-239120)

---

## What it does

- **Work orders** — create, view, edit and soft-delete service orders, each one
  tied to a vehicle, an oil, an additive and the set of filters that were
  replaced.
- **Faster order entry** — typing a licence plate auto-fills the last known data
  for that vehicle (owner, phone, model, previous service) so repeat visits take
  seconds.
- **Customers & vehicles** — people and their cars, related one-to-many.
- **Catalog management** — full create/update/delete for oils and additives,
  with brand and name normalization for the order-entry drop-downs.
- **Main dashboard** — a searchable, sortable grid of every work order, with
  staged filtering (plate first, owner name as fallback) that keeps your scroll
  position and selection while you type.
- **Resilient by default** — any unhandled error is logged to a file next to the
  executable and shown to the user as a clear message instead of a crash.

---

## Context & engineering constraints

The shop's production machine runs **Windows 7**, a detail that only surfaced
late in development:

- The code was written targeting **.NET 8** to use current tooling and language
  features.
- Windows 7 supports only up to **.NET 6**, so the project is **pinned back to
  `net6.0-windows7.0` with EF Core 6 for the delivered Release**.
- Nothing depends on APIs newer than .NET 6, so moving the target framework
  forward is a one-line change once the shop's environment is upgraded.

The **database schema is owned by the business**, not by this application. The
entity classes and `DbContext` are **scaffolded from the live database** with
[EF Core Power Tools](https://github.com/ErikEJ/EFCorePowerTools) — the app
adapts to the schema instead of migrating it.

---

## Architecture

Layered, with a single dependency direction:

```
Forms  ──►  Services  ──►  Repositories  ──►  OrdenesContext (EF Core)  ──►  SQL Server
(WinForms)   (business      (data access,      (scaffolded from the
             logic,          LINQ queries)      existing database)
             projections)
```

- **Repository + Service pattern** behind interfaces — the UI never touches
  EF Core directly.
- **Manual composition root** in `Program.cs` — dependencies are wired by hand
  and injected through constructors, deliberately kept container-free.
- **`IDbContextFactory<OrdenesContext>`** — every repository call creates and
  disposes its own short-lived `DbContext`, avoiding stale change-tracker state
  in a long-running desktop process.
- **DTO projections** — queries project straight into DTOs so each screen pulls
  only the columns it needs.
- **Async data access** end to end, with **nullable reference types** enabled.

### Project layout

```
LubricentroVelezV2/
├── Program.cs        # Entry point, composition root, global error handling
├── Forms/            # WinForms views (dashboard, work order, catalog dialogs)
├── Services/         # Interface + implementation — business logic
├── Repositories/     # Interface + implementation — EF Core data access
├── Models/           # Scaffolded entities + OrdenesContext
└── DTOs/             # Query projections
```

---

## Domain model

| Entity            | Represents                                              |
| ----------------- | ------------------------------------------------------ |
| `Personas`        | Customers                                              |
| `Autos`           | Vehicles, each owned by a `Persona`                    |
| `OrdenesTrabajos` | Service work orders — the core entity                  |
| `Aceites`         | Engine oils catalog                                    |
| `Aditivos`        | Additives catalog                                      |
| `Filtros`         | Filters catalog                                        |
| `FiltrosXot`      | Join table: which filters were used on which work order |

A work order captures the full service detail: mileage, oil, additive, replaced
filters and free-text fields for the many fluids and checks a complete service
covers (brake fluid, coolant, gearbox, differentials, battery, wipers, and more).

---

## Tech stack

| Area        | Technology                                             |
| ----------- | ------------------------------------------------------ |
| Language    | C# 11                                                  |
| Runtime     | .NET 6 (`net6.0-windows7.0`) — developed on .NET 8     |
| UI          | Windows Forms                                          |
| Data access | Entity Framework Core 6 (SQL Server provider)          |
| Database    | Microsoft SQL Server (pre-existing schema, database-first) |

---

## Screenshots

> _Dashboard, work-order form and catalog dialogs._

<!-- Add images here, e.g.:
![Dashboard](docs/dashboard.png)
![Work order](docs/work-order.png)
-->

---

<sub>Personal, extracurricular project delivered to a private business. Not affiliated with any institution.</sub>
