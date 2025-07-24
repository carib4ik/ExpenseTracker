# ExpenseTracker

A simple web application for tracking personal expenses. Built with ASP.NET Core, PostgreSQL, Docker

## Technologies Used

### Backend:
- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- REST API
- Dependency Injection, DTOs, CORS
- Swagger / OpenAPI

## Project Structure

```
ExpenseTracker/
├── ExpenseTracker.API/           # ASP.NET Core API project
├── ExpenseTracker.Application/   # Application layer (DTOs, Interfaces)
├── ExpenseTracker.Infrastructure/# Infrastructure layer (EF Core, Services)
├── client/                       # React frontend
```

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/ExpenseTracker.git
cd ExpenseTracker
```

### 2. Configure the database

Create a PostgreSQL database and update the connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=expensetracker;Username=postgres;Password=yourpassword"
  }
}
```

Then run migrations:

```bash
cd ExpenseTracker.API
dotnet ef database update
```

### 3. Run the backend

```bash
dotnet run --project ExpenseTracker.API
```

### 4. Run the frontend

```bash
cd client
npm install
npm run dev
```
Visit: [http://localhost:5173](http://localhost:5173)

## Features

- Add / delete categories
- Add / delete / update expenses
- Get all expenses or by date range or by category
- API documentation with Swagger
- Statistics all / by category / by date


