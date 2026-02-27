# 🍳 Recipe API

A RESTful Web API built with ASP.NET Core for managing cooking recipes with full CRUD operations, search functionality, and difficulty filtering.

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Authors](#authors)

## 🎯 About

This project is a modern Recipe Management API that demonstrates clean architecture principles, dependency injection, and comprehensive testing practices. Built with .NET 10, it provides a robust foundation for managing recipes with ingredients, cooking instructions, and difficulty levels.

## ✨ Features

- ✅ Full CRUD operations for recipes
- 🔍 Search recipes by name or description
- 🎚️ Filter recipes by difficulty level (Easy, Medium, Hard)
- ✔️ Comprehensive input validation using Data Annotations
- 📝 Swagger/OpenAPI documentation
- 🧪 Unit tests with xUnit and Moq
- 🏗️ Clean layered architecture
- 💉 Dependency Injection throughout
- ⚡ Async/await patterns for optimal performance

## 🏗️ Architecture

The project follows a clean layered architecture with clear separation of concerns:

````````markdown
RecipeApi/
├── Controllers/
├── Services/
├── Repositories/
├── Models/
│   └── DTOs/
└── Program.cs

RecipeApi.Tests/
````````

## 🚀 Technologies

The project is built using the following technologies:

- **.NET 10**: The latest version of .NET for building cross-platform applications.
- **ASP.NET Core Web API**: Framework for building HTTP services.
- **Swagger / OpenAPI**: For API documentation and testing.
- **xUnit**: A testing tool for .NET.
- **Moq**: A mocking library for .NET.
- **Dependency Injection**: For achieving Inversion of Control (IoC).
- **Async/Await**: For asynchronous programming.

## ▶️ Getting Started

To run the project locally, follow these steps:

1. **Clone the repository**
   ```bash
   git clone <your-github-link>
   cd RecipeApi
   ```

2. **Run the API project**
   ```bash
   dotnet run --project RecipeApi
   ```

3. **Open Swagger**
   Navigate to:
   - `https://localhost:7228/swagger/index.html`
   - `http://localhost:5129/swagger`
   
   Here you can test all endpoints directly.

## 📡 API Endpoints

The API provides the following endpoints:

- **Get all recipes**
  - `GET /api/recipes`
  
- **Get recipe by ID**
  - `GET /api/recipes/{id}`
  
- **Search recipes**
  - `GET /api/recipes/search?q={term}`
  
- **Filter by difficulty level**
  - `GET /api/recipes/difficulty/{level}`
    - Allowed values: Easy, Medium, Hard

- **Create a new recipe**
  - `POST /api/recipes`
  - Example request body:
    ```json
    {
      "name": "Pancakes",
      "description": "Classic Swedish pancakes",
      "prepTimeMinutes": 10,
      "cookTimeMinutes": 20,
      "servings": 4,
      "difficulty": "Easy",
      "ingredients": [
        { "name": "Flour", "quantity": 3, "unit": "dl" }
      ],
      "instructions": [
        "Mix ingredients",
        "Cook in a pan"
      ]
    }
    ```
  - Expected response:
    - `201 Created`
    - `Location: /api/recipes/{id}`

- **Update a recipe**
  - `PUT /api/recipes/{id}`
  - Expected responses:
    - `204 No Content`
    - `404 Not Found`

- **Delete a recipe**
  - `DELETE /api/recipes/{id}`
  - Expected responses:
    - `204 No Content`
    - `404 Not Found`

## 🧪 Testing

The project includes unit tests for both the service and controller layers.

- **Service Tests**: Validate business logic, mocking repositories with Moq.
- **Controller Tests**: Validate HTTP response codes and integration with services.

To run the tests, navigate to the solution folder and execute:

```bash
dotnet test
```

All tests should pass.

## 📂 Project Structure

The project is organized into the following key directories:

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Contain business logic and application rules.
- **Repositories**: Manage data access and persistence.
- **Models**: Define data structures and DTOs.

## 📬 Authors

This project was developed as a laboratory exercise in the course **Web API & Unit Testing**, through pair programming.

Contributors: **Aygen, Marika, Sandra & Tsoler**
