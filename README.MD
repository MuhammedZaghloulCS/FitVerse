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



---

## ⚙️ Technologies

- **ASP.NET MVC 6+**
- **Entity Framework Core**
- **SQL Server**
- **C#**
- **Bootstrap / jQuery (Frontend)**
- **Identity Roles (Admin, Coach, Client)**

---

##🧭 Some Features in the project
Integrated payment gateway for coach packages.

Statistics dashboard for coaches (sales, progress tracking).

Notification system (email/push).

##🧭 Future Enhancements

Mobile app version using the same API backend.
