# E-Commerce.API

## Overview
E-Commerce.API is a robust and scalable backend service for an online shopping platform. Built using **ASP.NET Core**, it provides essential features for managing products, orders, users, and payments.

## Features
- Product Catalog (Categories, Brands, Types)
- User Authentication & Authorization (JWT)
- Shopping Cart & Order Management
- Payment Processing (Stripe Integration)
- API Documentation with Swagger
- Entity Framework Core & SQL Server Database

## Technologies Used
- **ASP.NET Core** - Web API Framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database Management System
- **Swagger (Swashbuckle)** - API Documentation
- **AutoMapper** - Object Mapping
- **Stripe API** - Payment Integration
- **JWT Authentication** - Secure API Access

## Installation & Setup
### Prerequisites
- .NET SDK (Latest Version)
- SQL Server
- Visual Studio / VS Code
- Postman (For API Testing)

### Steps to Run the Project
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo/e-commerce-api.git
   cd e-commerce-api
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Update database with migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the API:
   ```sh
   dotnet run
   ```
5. Access Swagger documentation:
   ```
   http://localhost:<port>/swagger/index.html
   ```


## Contributing
1. Fork the repository.
2. Create a new branch: `git checkout -b feature-name`.
3. Commit changes: `git commit -m "Added feature"`.
4. Push the branch: `git push origin feature-name`.
5. Open a Pull Request.

## License
This project is open-source and available under the **MIT License**.

---
### Contact
For any issues or feature requests, open an issue on GitHub.

