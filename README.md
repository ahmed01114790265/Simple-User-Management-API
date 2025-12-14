# UserApi - Simple User Management Web API

**Description**
A simple ASP.NET Core Web API for user management (CRUD). Built as a project submission that demonstrates:
- CRUD endpoints (GET/POST/PUT/DELETE)
- Input validation using DataAnnotations
- Simple middleware: request logging and API key auth
- In-memory data store for demonstration

**How to run**
1. Install .NET 8 SDK.
2. `dotnet restore`
3. `dotnet run`
4. Open Swagger at `http://localhost:5000/swagger` (or the port shown in console).

**Endpoints**
- `GET /api/users` - get all users
- `GET /api/users/{id}` - get user by id
- `POST /api/users` - create user (requires X-API-KEY header for write operations)
- `PUT /api/users/{id}` - update user (requires X-API-KEY)
- `DELETE /api/users/{id}` - delete user (requires X-API-KEY)

**Validation**
Uses DataAnnotations. Invalid inputs return `400 Bad Request` with validation details.

**Middleware**
- RequestLoggingMiddleware: logs method, path and elapsed time.
- ApiKeyAuthMiddleware: protects POST/PUT/DELETE with header `X-API-KEY: secret-sample-key` (change before production).

**About Copilot**
I used GitHub Copilot to assist with writing and debugging some code sections (notably middleware and controller patterns). See commit history for notes.

**License**
MIT
# Simple User Management API
