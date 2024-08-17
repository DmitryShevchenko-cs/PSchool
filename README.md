# Api Documentation

## Overview
This API provides endpoints to manage students and their parents. The API allows you to create, retrieve, update, and delete student and parent data. The API is designed to be used by a frontend application or other services that require access to student and parent information.

## Getting Started
### Prerequisites
To run this API, you'll need the following:
- .NET 8 SDK
- SQL SERVER

## Installation
1. Update the database connection string in appsettings.json or appsettings.Development.json:
    ```
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
      }
    }
    ```
2. Run the database migrations
    ```
    dotnet ef database update
    ```

## Endpoints
### Students
- GET /students: Retrieve a list of students with optional pagination.
- GET /students/{id}: Retrieve details of a specific student by ID.
- POST /students: Create a new student.
- PUT /students: Update student by update model.
- DELETE /students/{id}: Delete a student by ID.

### Parents
- GET /parents: Retrieve a list of parents with optional pagination.
- GET /parents/{id}: Retrieve details of a specific parent by ID.
- POST /parents: Create a new parent.
- PUT /parents: Update an existing parent by update model.
- DELETE /parents/{id}: Delete a parent by ID.

### Pagination
When retrieving lists of students or parents, you can use pagination parameters:
- CurrentPage: The current page number.
- PageSize: The number of items per page.
 
