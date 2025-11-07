TalkMate
TalkMate is a personal counseling chatbot MVP built with ASP.NET Core Web API using Clean Architecture.
It allows users to send messages and receive simple, friendly responses, while storing the conversation in SQL Server.
This project is designed as a fully layered MVP, ready for testing, development, and extension.
________________________________________
🗂 Project Structure
TalkMate/
├── TalkMate.API           # Web API layer (Controllers, Swagger, Dependency Injection)
├── TalkMate.Application   # UseCases, Services, Interfaces, DTOs
├── TalkMate.Domain        # Domain entities (User, Message, ChatResponse) and domain logic
└── TalkMate.Infrastructure # DbContext, Repositories, and external service integrations
•	Domain – Contains all business entities and core domain logic.
•	Application – Contains use cases, service interfaces, and business workflows.
•	Infrastructure – Implements data access using EF Core, repositories, and other external services.
•	API – Entry point for REST APIs, dependency injection, and Swagger.
________________________________________
⚙️ Technologies
•	.NET 8 
•	C#
•	ASP.NET Core Web API
•	Entity Framework Core
•	SQL Server (LocalDB)
•	REST API
•	Async/Await
•	Repository Pattern
•	Dependency Injection
•	Swagger
________________________________________
💬 Features / Endpoints
1.	POST /api/chat/send
o	Send a message to the chatbot
o	Input: userId, messageText
o	Output: Chatbot response
o	Logic: simple keyword-based response
	"stress" → "It seems you're stressed. Take a deep breath 🌿"
	"happy" → "That's great! Keep positive energy 😊"
	Else → "I'm here to listen. Tell me more about how you feel."
2.	GET /api/chat/history/{userId}
o	Get the chat history for a specific user
o	Output: List of user messages and chatbot responses
________________________________________
🏃‍♂️ Getting Started
Prerequisites
•	.NET 8 SDK 
•	SQL Server or LocalDB
•	Optional: Postman or browser for testing API
Setup
1.	Clone the repository:
git clone https://github.com/yourusername/TalkMate.git
cd TalkMate
2.	Update connection string in Infrastructure/ChatbotDbContext.cs or appsettings.json:
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TalkMateDb;Trusted_Connection=True;"
}
3.	Apply EF Core migrations:
cd TalkMate.Infrastructure
dotnet ef database update
4.	Run the API:
cd TalkMate.API
dotnet run
5.	Open Swagger to test endpoints:
https://localhost:5001/swagger/index.html
________________________________________
🧩 Architecture & Design
•	Clean Architecture with clear separation of Domain, Application, Infrastructure, and API layers.
•	Entities reside in Domain; DbContext and repository implementations reside in Infrastructure.
•	Application layer contains business logic and interfaces for repositories.
•	API layer only interacts with Application services and handles HTTP requests/responses.
________________________________________
✅ Future Improvements
•	Add authentication & authorization (JWT or OAuth2)
•	Enhance chatbot logic (AI/NLP integration)
•	Add unit and integration tests
•	Dockerize the application for easier deployment
•	Add CI/CD pipeline
________________________________________
📝 Notes
•	This is a Minimum Viable Product (MVP) but fully layered and structured for easy extension.
•	Example user and sample messages are seeded in the database for testing.
________________________________________
👤 Author
•	Atiye Dadbam
•	GitHub: atiye-dm
•	LinkedIn: linkedin.com/in/atiye-dadbam

