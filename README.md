# SistemaDeGesti-nDeTurnosYAfiliados
A web application built with ASP.NET Core 8.0 for managing affiliates and appointments efficiently.
It uses Entity Framework Core, supports both MySQL and SQLite, and integrates SignalR for real-time updates.

🚀 Main Features

Full CRUD for affiliates and appointments

Configurable database (switch between MySQL or SQLite) through appsettings.json

Layered architecture with:

MVC Controllers

Generic Repositories

Domain Services

Real-time communication using SignalR

Ready for development and production environments

⚙️ Technologies Used
Technology	Purpose
ASP.NET Core 8.0 MVC	Web framework (Controllers & Razor views)
Entity Framework Core	ORM for database operations
MySQL / SQLite	Database engines
SignalR	Real-time client-server communication
Dependency Injection (DI)	Repository and service injection
Pomelo.EntityFrameworkCore.MySql	MySQL provider
Microsoft.EntityFrameworkCore.Sqlite	SQLite provider
🧩 Project Structure
systemdeeps.WebApplication/
│
├── Controllers/          → MVC controllers
├── Models/               → Domain entities
├── Data/                 → DbContext and migrations
├── Repositories/         → Generic repository implementation
├── Services/             → Business logic (TurnService, AffiliateService)
├── Hubs/                 → Real-time communication (SignalR)
├── Views/                → Razor views
├── wwwroot/              → Static files
├── Program.cs            → Application startup configuration
├── appsettings.json      → Main configuration file
└── appsettings.Development.json → Development environment settings

🧠 Database Configuration

The appsettings.json file defines the connection strings:

"ConnectionStrings": {
  "MySql": "Server=127.0.0.1;Port=3306;Database=systemdeepsdb;User=root;Password=126566;",
  "Sqlite": "Data Source=systemdeeps.db"
},
"UseMySql": false


When "UseMySql": true, the app connects to MySQL.

When "UseMySql": false, it uses SQLite (systemdeeps.db) locally.

🧰 Setup and Run

Clone the repository

git clone https://github.com/RafaeRamirez/SistemaDeGesti-nDeTurnosYAfiliados.git
cd SistemaDeGesti-nDeTurnosYAfiliados/systemdeeps.WebApplication


Restore dependencies

dotnet restore


Apply migrations

dotnet ef database update


Run the project

dotnet run


Open in your browser

https://localhost:5001

🔄 Real-Time Communication

The system uses SignalR via the /turnHub endpoint to synchronize turn states and notify connected clients in real time.

👨‍💻 Author

Rafael Ramírez
Full Stack .NET Developer
📧 rafar1129@gmail.com

💻 GitHub: RafaeRamirez