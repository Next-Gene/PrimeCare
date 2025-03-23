# PrimeCare

PrimeCare is a .NET 8-based application designed to provide a robust healthcare management platform. This solution includes multiple projects targeting different aspects of the application, such as the domain, application services, and infrastructure.

## Features

- Manage patients, appointments, and medical records
- CRUD operations for patients, appointments, and medical records
- Integration with Entity Framework Core
- Automatic retries for transient failures
- Swagger integration for API documentation
- Identity and JWT authentication support
- Enhanced logging with Serilog

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/Next-Gene/PrimeCare.git
    ```
2. Navigate to the project directory:
    ```sh
    cd PrimeCare
    ```
3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

### Configuration

1. Update the connection string in `appsettings.json`.

### Usage

1. Apply the migrations and update the database:
    ```sh
    dotnet ef database update
    ```
2. Run the application:
    ```sh
    dotnet run
    ```

### Project Structure

- **PrimeCare.Domain**: Contains the domain entities and business logic.
- **PrimeCare.Application**: Implements application services and business rules.
- **PrimeCare.Infrastructure**: Handles data access and database migrations.
- **PrimeCare.Host**: The entry point of the application, including API endpoints.

## Dependencies

- AutoMapper (14.0.0)
- Microsoft.EntityFrameworkCore (8.0.14)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.14)
- Microsoft.EntityFrameworkCore.Tools (8.0.14)
- Microsoft.Extensions.Configuration.Abstractions (9.0.3)
- Swashbuckle.AspNetCore (6.6.2)
- Serilog (2.10.0)
- FluentValidation (11.2.2)
- FluentValidation.AspNetCore (11.2.2)

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.

### License

This project is licensed under the MIT License.
