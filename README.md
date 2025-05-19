## HR Management Company System

A full-stack, production-ready **HR Management System** built with **ASP.NET Core MVC**, applying industry-standard architecture and design patterns. This system is designed to streamline HR operations like employee and department management, user access control, and more — now **live on Monster ASP**.

## 🌐 Live Demo

🔗 [View Live on Monster ASP](http://company-hr-management-system.runasp.net/Account/SignIn?ReturnUrl=%2F)
---
🧰 Tech Stack

- ASP.NET Core MVC (.NET 6/7+)
- Entity Framework Core (EF Core)
- SQL Server
- ASP.NET Identity (authentication & role management)
- Bootstrap (frontend styling)
- AutoMapper, **Dependency Injection**
- Generic Repository & Unit of Work
---

 🔐 Authentication & Authorization
- Role-based access control with ASP.NET Identity
- Secure login, registration, and user management
- Predefined roles (Admin, HR, Manager, etc.)
---

 🧩 Clean Architecture
Follows a 3-layered architectural pattern for maintainability and scalability:

Presentation Layer ➡️ Business Logic Layer ➡️ Data Access Layer

- Generic Repository Pattern for reusable data access logic
- Unit of Work for managing database transactions
- AutoMapper for seamless Model ↔️ DTO mapping
- Dependency Injection for loose coupling and easier testing
---
 📊 Key Features

 👥 Employees
- Create, update, delete, and soft-delete employees
- Upload and manage employee profile pictures
- Assign employees to departments
- Search and filter employee records

 🏢 Departments
- Full CRUD operations for departments
- View department-wise employee lists

 🧑‍💼 User Management
- Register and manage users
- Assign roles and permissions
- Secure session handling

 📁 File Handling
- Upload, store, and manage employee photos
- Image preview in employee profiles
