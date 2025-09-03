# TodoListApp

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) installed
- Linux environment

## Common Commands

### Run Tests

cd TodoListApp.Tests &&
dotnet test

### Run Migrations (Infrastructure)

cd TodoListApp.API &&
dotnet tool run dotnet-ef migrations add migrateToDb -p ../TodoListApp.Infrastructure -s .

### Run the Application

cd TodoListApp.API &&
dotnet run
