# NätOnNät ChatterBox (Azure OpenAI)

Kort skolprojekt byggt med ASP.NET Core MVC + MSSQL + Entity Framework Core som använder Azure OpenAI för att driva en enkel chatt. Chathistorik visas på sidan (nyaste överst) och sparas lokalt i din SQL Server-instans på datorn där appen körs. Designen är inspirerad av NetOnNet.

## Tech/stack
- .NET 9 (ASP.NET Core MVC)
- MSSQL + Entity Framework Core (Code‑First, migrering vid uppstart)
- Azure OpenAI (Chat)
- Bootstrap 5 (mobile‑first)

## Krav och funktioner
- Visa hela chathistoriken (prompt + respons), nyaste överst.
- Validering: tomma meddelanden blockeras; längdbegränsning 1000 tecken.
- Typindikator (“AI skriver…”) och smidig scroll.
- Mobile‑first layout.

## Kom igång

### Förutsättningar
- .NET 9 SDK
- SQL Server (LocalDB eller valfri lokal/instans)
- Azure OpenAI-nyckel + ett Chat-deployment

### Konfiguration

Miljövariabler (Windows PowerShell):
```powershell
setx AZURE_OPENAI_ENDPOINT "https://<din-endpoint>.openai.azure.com"
setx OPENAIKEY "<din_api_nyckel>"
setx AZURE_OPENAI_DEPLOYMENT_NAME "<ditt_chat_deployment_namn>"
```

## Projektstruktur
- `Controllers/ChatController.cs` – webblogik (Index + SendMessage).
- `Features/Chat/*` – handlers och DTO:er (OOP-upplägg).
- `Infrastructure/Ai/AiService.cs` – samtal mot Azure OpenAI (persona och chattlogik).
- `Data/ChatterBoxDbContext.cs` – EF Core DbContext.
- `Views/Chat/Index.cshtml` – chattsida (JS inline).
- `Migrations/` – EF-migreringar.

