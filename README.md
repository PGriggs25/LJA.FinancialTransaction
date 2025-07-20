# LJA FinancialTransaction API

A simple ASP.NET Core Web API for managing financial transactions, built with Entity Framework Core and automatic migrations.

## Technologies
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)  
- [ASP.NET Core Web API](https://docs.microsoft.com/aspnet/core/web-api)  
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)  
- Microsoft SQL Server (or any EF Core–supported database provider)


## Getting Started
Clone the repo:
git clone [https://github.com/PGriggs25/LJA.FinancialTransaction.git)


## Configure Database
Update the LJAConnection value to point at your SQL Server instance.


## Database & Migrations
The API is set up to apply any pending migrations at runtime.
