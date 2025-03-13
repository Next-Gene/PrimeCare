# eCommerceApp

eCommerceApp is a .NET 8 based application designed to manage an e-commerce platform. It includes functionalities for managing products, categories, and more.

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

- `eCommerceApp.Application`: Contains application logic and DTOs.
- `eCommerceApp.Domain`: Contains domain entities and interfaces.
- `eCommerceApp.Infrastructure`: Contains data access logic and repository implementations.
- `eCommercecApp.Host`: The entry point of the application.

### Contributing

Contributions are welcome! Please open an issue or submit a pull request.

### License

This project is licensed under the MIT License.
   
