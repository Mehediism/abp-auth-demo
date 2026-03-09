# ABP Auth Demo

ABP Framework **authentication** and **permission-based authorization** demo: scaffolded module structure with permission definitions, application services, and HTTP API protected by permissions.

## Contents

- **article.md** — Technical article: Authentication vs Authorization in ABP, permission-based access control, role management, and code examples.
- **src/** — Layered ABP-style solution (Domain.Shared, Domain, Application.Contracts, Application, HttpApi) with permissions and `[Authorize]` usage.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An ABP host application (e.g. [ABP CLI](https://docs.abp.io/en/abp/latest/CLI) generated app) that references this solution or its projects.

## Project structure

| Project | Purpose |
|--------|---------|
| **AbpAuthDemo.Domain.Shared** | Localization, constants; no domain logic. |
| **AbpAuthDemo.Domain** | Domain module (empty except module class). |
| **AbpAuthDemo.Application.Contracts** | Permission definitions (`AbpAuthDemoPermissionDefinitionProvider`, `AbpAuthDemoPermissions`), DTOs, `IItemAppService`. |
| **AbpAuthDemo.Application** | `ItemAppService` with `[Authorize(AbpAuthDemoPermissions.Items.*)]`. |
| **AbpAuthDemo.HttpApi** | `ItemController` with permission-based `[Authorize]` on controller and actions. |

## Setup and usage

### 1. Open the solution

```bash
cd abp-auth-demo
dotnet restore
dotnet build
```

### 2. Integrate into an ABP host

- Create a new ABP application (e.g. `abp new MyApp`) or use an existing one.
- Add project references to **AbpAuthDemo.Application.Contracts**, **AbpAuthDemo.Application**, and **AbpAuthDemo.HttpApi** (and optionally Domain/Domain.Shared if not referenced transitively).
- In the host's module class, add `[DependsOn(typeof(AbpAuthDemoHttpApiModule))]` (and `AbpAuthDemoApplicationModule` if the host references the Application layer directly).
- Ensure the host has **Identity** and **Permission Management** modules so users/roles and permission assignments work.

### 3. Permissions

- **AbpAuthDemo** group: **AbpAuthDemo.Items** (default), **AbpAuthDemo.Items.Create**, **AbpAuthDemo.Items.Edit**, **AbpAuthDemo.Items.Delete**.
- Assign these to roles or users via the Permission Management UI (Identity → Roles / Users → Permissions) or in code with `IPermissionManager.SetForRoleAsync` / `SetForUserAsync`.

### 4. API

- `GET /api/abp-auth-demo/items/{id}` — requires **AbpAuthDemo.Items**.
- `POST /api/abp-auth-demo/items` — requires **AbpAuthDemo.Items** and **AbpAuthDemo.Items.Create**.

Call these with a bearer token (or cookie) for an authenticated user that has the required permissions.

## References

- [ABP Authorization](https://abp.io/docs/latest/framework/fundamentals/authorization)
- [Permission Management Module](https://abp.io/docs/latest/modules/permission-management)
- [Identity Module](https://abp.io/docs/latest/modules/identity)
- [Building a Permission-Based Authorization System (ABP Community)](https://abp.io/community/articles/building-a-permissionbased-authorization-system-for-asp.net-core-owyszy0b)
