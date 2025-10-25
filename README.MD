# 🏋️‍♂️ FitVerse

**FitVerse** is a web-based fitness management platform built with **ASP.NET MVC** and **SQL Server**.  
It provides a complete environment where **Admins**, **Coaches**, and **Clients** can interact through role-based access and structured training management.

---

## 🚀 Overview

FitVerse connects three main user roles:

### 🧑‍💼 Admin
- Manages the entire system and its content.  
- Adds and upgrades users (from *Client* to *Coach* or *Admin*).  
- Oversees all platform data and user activities.

### 🏋️ Coach
- Responsible for training programs and client supervision.  
- Creates exercises, workout plans, and manages paid/free packages.  
- Tracks clients’ progress and provides guidance.

### 👤 Client
- Accesses free workouts and fitness content.  
- Can browse available **packages** and **subscribe** to their preferred coach.  
- Enjoys a personalized training experience for a paid subscription.

---

## 🧩 Project Structure

The solution follows a clean, layered architecture:

FitVerse.sln
├─ FitVerse.Core # Core models, DTOs, interfaces
├─ FitVerse.Data # Database context, migrations, repositories
├─ FitVerse.Service # Business logic and services
└─ FitVerse.Web # MVC layer (controllers, views, wwwroot)

yaml
Copy code

---

## ⚙️ Technologies

- **ASP.NET MVC 6+**
- **Entity Framework Core**
- **SQL Server**
- **C#**
- **Bootstrap / jQuery (Frontend)**
- **Identity Roles (Admin, Coach, Client)**

---

## 🛠️ Setup & Installation

### 1. Clone the repository
```bash
git clone <repository-url>
cd FitVerse
2. Configure the database connection
Edit appsettings.json inside FitVerse.Web:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SQL_SERVER;Database=FitVerseDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Replace YOUR_SQL_SERVER with your actual SQL instance name (e.g., localhost, (localdb)\\MSSQLLocalDB, or a remote server).

3. Restore dependencies & build the solution
bash
Copy code
dotnet restore
dotnet build
4. Apply database migrations (if applicable)
bash
Copy code
cd FitVerse.Data
dotnet ef database update --startup-project ../FitVerse.Web
Alternatively, run the provided SQL script if available.

5. Run the application
bash
Copy code
cd FitVerse.Web
dotnet run
Open your browser and navigate to:
👉 https://localhost:5001 or the port shown in the console.

🔑 Default Admin Credentials
If you encounter:

pgsql
Copy code
Default admin credentials are missing in configuration.
Make sure you’ve defined them in appsettings.json:

json
Copy code
"DefaultAdmin": {
  "Email": "admin@example.com",
  "Password": "YourStrongP@ssw0rd"
}
Or configure them as environment variables before running the app.

🧱 Database & Migrations
Database context is located in FitVerse.Data.

To create a new migration:

bash
Copy code
dotnet ef migrations add <MigrationName> --startup-project ../FitVerse.Web
Then update the database:

bash
Copy code
dotnet ef database update
🔒 Security & Roles
Uses ASP.NET Identity for authentication and role-based authorization.

Admin-only pages are protected by role checks.

CSRF protection enabled via Anti-Forgery tokens.

Validation applied on all input forms.

🌐 Deployment Tips
Store connection strings and admin credentials as environment variables.

Always use HTTPS and enable HSTS.

Add environment-specific settings in appsettings.Production.json.

Backup your database before publishing updates.

🤝 Contributing
Fork the repository.

Create a new branch:

bash
Copy code
git checkout -b feature/my-feature
Commit your changes with clear messages.

Submit a Pull Request (PR) describing your work.

🧭 Some Features in the project
Integrated payment gateway for coach packages.

Statistics dashboard for coaches (sales, progress tracking).

Notification system (email/push).

🧭 Future Enhancements
Mobile app version using the same API backend.