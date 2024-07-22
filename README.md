# Backend System for Course Management Application

## Project Overview

This project focuses on the backend development of a web application for managing courses, modules, and users at OsloMet. The backend is built using ASP.NET Core, providing APIs for various functionalities including user authentication, module management, and data handling with SQL Server.

## Key Features

### Database Management

- **Technology**: SQL Server was chosen for its robustness and compatibility with Azure, allowing for local and remote database management.
- **ORM**: Initially, Entity Framework was considered, but later Dapper was used for its simplicity and direct control over SQL queries.
- **Database Structure**: The database includes six tables (`Person`, `Admin`, `Teacher`, `Module`, `Module-Person`, `Lesson`) and 25 stored procedures for efficient data operations.

### API Development

- **Framework**: ASP.NET Core framework was used to build RESTful APIs.
- **Endpoints**: Developed endpoints for CRUD operations on modules, lessons, teachers, and user management.
- **Authentication and Authorization**:
  - Implemented JWT (JSON Web Token) for secure authentication.
  - Used claims-based identity for flexible authorization models.
  - Role-Based Access Control (RBAC) was applied to differentiate access levels between admins and regular users.

### Authentication and Authorization

- **JWT**: JSON Web Token is used to securely transfer user information.
- **Claims-Based Identity**: Utilized for managing user roles and permissions, ensuring that only authorized users can perform specific actions.
- **Implementation**: Configured in ASP.NET Core with middleware for handling authentication and authorization logic.

### Data Handling

- **Dapper**: Chosen for its efficiency and simplicity, allowing direct SQL operations.
- **Stored Procedures**: Used for all database interactions to ensure consistency and control.

## Project Structure

- **Controllers**: Handle HTTP requests and interact with services and repositories.
- **Repositories**: Manage data access logic and interactions with the database using Dapper.
- **Models**: Define data structures and entities within the application.

