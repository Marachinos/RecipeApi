🍳 Recipe API – Web API & Enhetstester
📌 Beskrivning

Detta projekt är ett RESTful Web API byggt med ASP.NET Core för att hantera matrecept.
API:et stödjer fullständiga CRUD-operationer samt sökning och filtrering.

Projektet är uppbyggt enligt lagerarkitektur med:

Controllers (HTTP-logik)

Services (affärslogik)

Repositories (dataåtkomst)

DTOs med validering

Dependency Injection

Enhetstester med xUnit och Moq

🏗️ Arkitektur
RecipeApi/
├── Controllers/
├── Services/
├── Repositories/
├── Models/
│   └── DTOs/
└── Program.cs

RecipeApi.Tests/
Lageransvar
Lager	Ansvar
Controller	Hanterar HTTP-anrop och statuskoder
Service	Innehåller affärslogik (Search, Difficulty, validering)
Repository	Hanterar data (in-memory lagring)
DTO	Validering via DataAnnotations
🚀 Tekniker

.NET 8

ASP.NET Core Web API

Swagger / OpenAPI

xUnit

Moq

Dependency Injection

Async/Await

▶️ Hur man kör projektet
1️⃣ Klona repository
git clone <din-github-länk>
cd RecipeApi
2️⃣ Kör API-projektet
dotnet run --project RecipeApi
3️⃣ Öppna Swagger

Navigera till:

https://localhost:7228/swagger/index.html
http://localhost:5129/swagger
Där kan du testa alla endpoints direkt.

🧪 Hur man kör tester

Gå till solution-mappen och kör:

dotnet test

Alla tester ska passera.

📡 API Endpoints
🔹 Hämta alla recept
GET /api/recipes
🔹 Hämta recept via ID
GET /api/recipes/{id}
🔹 Sök recept
GET /api/recipes/search?q={term}
🔹 Filtrera på svårighetsgrad
GET /api/recipes/difficulty/{level}

Tillåtna värden:

Easy

Medium

Hard

🔹 Skapa nytt recept
POST /api/recipes

Exempel:

{
  "name": "Pannkakor",
  "description": "Klassiska svenska pannkakor",
  "prepTimeMinutes": 10,
  "cookTimeMinutes": 20,
  "servings": 4,
  "difficulty": "Easy",
  "ingredients": [
    { "name": "Mjöl", "quantity": 3, "unit": "dl" }
  ],
  "instructions": [
    "Blanda ingredienser",
    "Stek i panna"
  ]
}

Svar:

201 Created
Location: /api/recipes/{id}
🔹 Uppdatera recept
PUT /api/recipes/{id}

Returnerar:

204 NoContent

404 NotFound

🔹 Ta bort recept
DELETE /api/recipes/{id}

Returnerar:

204 NoContent

404 NotFound

✅ Validering

Validering sker via DataAnnotations i DTO-klasser:

Name: Required, min 3 tecken

PrepTimeMinutes: 1–480

CookTimeMinutes: 0–480

Servings: 1–100

Ingredients: minst 1

Instructions: minst 1

Difficulty: Easy / Medium / Hard

Vid ogiltig data returnerar API:

400 Bad Request
🧪 Enhetstester
Service-tester

Testar:

GetAll

GetById (existerande)

GetById (saknas)

Create

Search

Repository mockas med Moq.

Controller-tester

Testar:

GetAll → 200 OK

GetById → 404

Create → 201 Created

Service mockas i controllertester.

📌 Designbeslut

In-memory repository används istället för databas.

Repository innehåller endast CRUD (dataåtkomst).

Service innehåller affärslogik (Search, Difficulty-filter).

Async/await används genomgående.

Dependency Injection konfigureras i Program.cs.

🏆 Bedömning
Uppfyller Godkänt (G)

Alla endpoints implementerade

Lagerarkitektur korrekt

Validering implementerad

Minst 8 enhetstester

Dependency Injection används korrekt

📬 Författare

Laboration i kursen Web API & Enhetstester.
Utvecklat med parprogrammering.
