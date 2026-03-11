# Handmade Shop API 🛍️

A robust and scalable backend RESTful API built for an E-commerce platform. This project serves as the core engine for managing products, user authentication, real-time communications, and high-performance shopping cart operations.

## ✨ Key Features

* **Authentication & Authorization:** Secure user login and role-based access control using JWT (JSON Web Tokens).
* **High-Performance Cart:** Integrated **Redis** for blazingly fast and distributed shopping cart state management.
* **Real-time Capabilities:** Implemented **SignalR** for real-time customer support chat (1-1 with Admin) and instant system notifications.
* **Solid Architecture:** Structured using **Repository** and **Unit of Work** design patterns for maintainability, decoupling, and efficient database transactions.
* **Image Handling:** Multipart/form-data support for user avatars and product image uploads.

## 🛠️ Tech Stack

* **Framework:** .NET Core (C#)
* **Database:** SQL Server (via Entity Framework Core)
* **Caching:** Redis
* **Real-time:** ASP.NET Core SignalR
* **Security:** JWT Bearer Authentication

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.

### Prerequisites
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/handmade-shop-api.git
2. Build and run the project using Docker Compose:
	docker-compose up -d --build
Note: The API will be available at http://localhost:5000/swagger.
📡 API Endpoints Overview
Here is a quick overview of the main API resources:
Method,Endpoint,Description,Auth Required
POST,/api/auth/login,Authenticate user and get JWT,No
POST,/api/auth/register,Register a new user,No
GET,/api/products,Get list of products,No
GET,/api/cart,Get current user's Redis cart,Yes
POST,/api/cart,Add item to Redis cart,Yes