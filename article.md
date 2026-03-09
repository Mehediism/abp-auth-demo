# ABP Framework: Authentication & Authorization

This technical article covers **authentication** vs **authorization** in ABP Framework, **permission-based access control**, **role management**, and practical code examples. It is based on [ABP documentation](https://abp.io/docs/latest/framework/fundamentals/authorization) and the community article [Building a Permission-Based Authorization System for ASP.NET Core](https://abp.io/community/articles/building-a-permissionbased-authorization-system-for-asp.net-core-owyszy0b).

---

## 1. Authentication vs Authorization in ABP

### Authentication

**Authentication** answers: *Who is the user?* It verifies identity (e.g. username/password, JWT, OpenID Connect).

In ABP:

- **Identity module** ([docs](https://abp.io/docs/latest/modules/identity)) handles users, roles, and credentials.
- **Account module** ([docs](https://abp.io/docs/latest/modules/account)) provides login, logout, and registration.
- **JWT** and **OpenIddict** ([docs](https://abp.io/docs/latest/modules/openiddict)) are used for API and SPA authentication.

Authentication is typically configured in the host (e.g. `AddJwtBearer`, cookie auth). ABP integrates with ASP.NET Core authentication and adds multi-tenancy and current-user abstractions (`ICurrentUser`).

### Authorization

**Authorization** answers: *What is the user allowed to do?* It runs *after* authentication and decides whether an action is allowed.

In ABP:

- Authorization is **permission-based**: you define permissions (e.g. `BookStore.Books.Create`) and assign them to users or roles.
- The framework extends [ASP.NET Core Authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/introduction) and builds on **policy-based** authorization.
- Permissions are registered as policies automatically, so you use the same `[Authorize("PermissionName")]` and `IAuthorizationService` patterns.

**Summary:**

| Concept        | Purpose                          | ABP building blocks                    |
|----------------|-----------------------------------|----------------------------------------|
| **Authentication** | Identify the user                 | Identity, Account, JWT, OpenIddict     |
| **Authorization**  | Decide allowed actions           | Permissions, policies, Permission Management module |

---

## 2. Permission-Based Access Control

ABP uses **permissions** as the main unit of authorization. Each permission is a named policy (e.g. `BookStore.Books.Delete`) that can be granted or revoked for users and roles.

### Defining permissions

Permissions are defined in a **Permission Definition Provider** (in `*.Application.Contracts`), which inherits from `PermissionDefinitionProvider`:

```csharp
public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup("BookStore");

        var booksPermission = bookStoreGroup.AddPermission("BookStore.Books", L("Permission:Books"));
        booksPermission.AddChild("BookStore.Books.Create", L("Permission:Books.Create"));
        booksPermission.AddChild("BookStore.Books.Edit", L("Permission:Books.Edit"));
        booksPermission.AddChild("BookStore.Books.Delete", L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
```

ABP discovers this class and registers each permission as a policy. You can then assign permissions via the **Permission Management** UI or via `IPermissionManager` in code.

### Assigning permissions (code)

Use `IPermissionManager` to grant or revoke permissions for a user or role:

```csharp
public class MyService : ITransientDependency
{
    private readonly IPermissionManager _permissionManager;

    public MyService(IPermissionManager permissionManager)
    {
        _permissionManager = permissionManager;
    }

    public async Task GrantPermissionForUserAsync(Guid userId, string permissionName)
    {
        await _permissionManager.SetForUserAsync(userId, permissionName, true);
    }

    public async Task ProhibitPermissionForUserAsync(Guid userId, string permissionName)
    {
        await _permissionManager.SetForUserAsync(userId, permissionName, false);
    }
}
```

For roles, use `SetForRoleAsync(roleName, permissionName, true/false)`.

### Checking permissions in application services and controllers

**Declarative:** use `[Authorize("PermissionName")]` on the class or method:

```csharp
[Authorize("BookStore.Books")]
public class BookAppService : ApplicationService, IBookAppService
{
    [Authorize("BookStore.Books.Create")]
    public async Task<BookDto> CreateAsync(CreateBookDto input)
    {
        // Only users with BookStore.Books and BookStore.Books.Create can execute this.
    }
}
```

```csharp
[Authorize("BookStore.Books")]
public class BookController : AbpController
{
    [Authorize("BookStore.Books.Create")]
    public async Task<IActionResult> Create(CreateBookViewModel model) { ... }
}
```

**Programmatic:** use `IAuthorizationService` (available as `AuthorizationService` on `ApplicationService`):

```csharp
public class BookAppService : ApplicationService, IBookAppService
{
    public async Task<BookDto> CreateAsync(CreateBookDto input)
    {
        await AuthorizationService.CheckAsync(BookStorePermissions.Books.Create);
        // Or: if (!await AuthorizationService.IsGrantedAsync(...)) { throw ... }
        // Your logic here
    }

    public async Task<bool> CanUserCreateBooksAsync()
    {
        return await AuthorizationService.IsGrantedAsync(BookStorePermissions.Books.Create);
    }
}
```

- `CheckAsync(permission)` — throws if not granted.
- `IsGrantedAsync(permission)` — returns `true`/`false`.
- `AuthorizeAsync(permission)` — returns `AuthorizationResult` with `Succeeded`.

---

## 3. Role Management

Roles are the primary way to group permissions and assign them to many users at once.

- **Identity module** provides **roles** and **users**; users get permissions either directly or through roles.
- **Permission Management module** ([docs](https://abp.io/docs/latest/modules/permission-management)) provides UI and API to assign permissions to roles and users.
- In the admin UI, Role Management and User Management pages include permission modals; permissions inherited from roles are often marked (e.g. with "(R)").

Best practices:

- Define a small set of **roles** (e.g. Admin, Manager, User) and assign **permissions** to roles.
- Use **user-level** permission overrides only when you need to grant or revoke a specific permission for a single user.
- Use constants for permission names (e.g. `BookStorePermissions.Books.Create`) to avoid typos and refactor safely.

---

## 4. References and further reading

- [ABP Framework – Authorization](https://abp.io/docs/latest/framework/fundamentals/authorization)
- [ABP Permission Management Module](https://abp.io/docs/latest/modules/permission-management)
- [ABP Identity Module](https://abp.io/docs/latest/modules/identity)
- [Building a Permission-Based Authorization System for ASP.NET Core (ABP Community)](https://abp.io/community/articles/building-a-permissionbased-authorization-system-for-asp.net-core-owyszy0b)
- [ABP Documentation (all categories)](https://abp.io/docs/latest) — Getting Started, Tutorials, Framework, Modules, UI, Deployment

---

*Generated with ABP Community MCP tools. Replace placeholder links with the exact doc version (e.g. 8.2 or latest) as needed.*
