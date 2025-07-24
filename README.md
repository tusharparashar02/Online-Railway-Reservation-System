# 🚆 Online Railway Reservation System

A full-stack web application built to streamline the railway ticket reservation process across the Indian Railways network. This platform allows passengers to book, manage, and cancel tickets online with features like PDF ticket downloads, catering and wellness kit bookings, and a clean, responsive UI.

## 🛠️ Tech Stack

- **Frontend**: Angular 19, Tailwind CSS, Animations
- **Backend**: .NET 8 Web API, Entity Framework Core
- **Database**: SQL Server
- **Authentication**: SMTP Email Verification & JWT

---

## ✨ Features

### 👥 Passenger Features
- Register and login using **SMTP-based email verification** and **JWT authentication**
- **Search trains** by source and destination
- **Book tickets** with class selection (Sleeper, AC, First AC)
- View **booking history** and **ticket status**
- **Download tickets as PDF** invoices
- **Order wellness kits** and **book catering (food)** for your journey
- Fully functional **passenger profile dashboard**

### 🔐 Admin Features (Swagger Only)
- Add, update, or delete **train details**
- Manage **schedules and halt timings**
- Set and update **fare details** for routes and classes

---

## 📂 Backend API Modules

- `AuthController`: Register/Login with SMTP & JWT
- `TrainController`: Train and schedule management
- `ReservationController`: Booking and ticket history
- `PassengerController`: Profile and ticket info
- `CateringOrderController`: CRUD for food orders
- `WellnessKitRequestController`: CRUD for in-train wellness kits

---

## 🖥️ Frontend

- Built using **Angular 19** with **Tailwind CSS** for responsive design
- Includes smooth animations and mobile-friendly components
- Integrates with backend APIs for real-time ticketing and data sync

---

## 🚀 Getting Started

### Prerequisites
- Node.js
- Angular CLI
- .NET 8 SDK
- SQL Server

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/tusharparashar/online-railway-reservation.git
