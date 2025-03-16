# eCommerceApp

eCommerceApp is a .NET 8 based application designed to provide a robust e-commerce platform. This solution includes multiple projects targeting different aspects of the application, such as the domain, application, infrastructure, and host layers.

## Features

- Manage products and categories
- CRUD operations for products and categories
- Integration with Entity Framework Core
- Automatic retries for transient failures
- Swagger integration for API documentation

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Restore the dependencies

### Configuration

1. Update the connection string in `appsettings.json`

### Usage

1. Apply the migrations and update the database
2. Run the application

### Project Structure

- **eCommerceApp.Domain**: Contains the domain entities and business logic.
- **eCommerceApp.Application**: Implements application services and business rules.
- **eCommerceApp.Infrastructure**: Handles data access and database migrations.
- **eCommerceApp.Host**: The entry point of the application, including API endpoints.

## Dependencies

- AutoMapper (14.0.0)
- Microsoft.EntityFrameworkCore (8.0.14)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.14)
- Microsoft.EntityFrameworkCore.Tools (8.0.14)
- Microsoft.Extensions.Configuration.Abstractions (9.0.3)
- Swashbuckle.AspNetCore (6.6.2)

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.

### License

This project is licensed under the MIT License.
   
