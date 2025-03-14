# LogisticsApi


# Environment Setup Guide for Last-Mile Delivery Platform API Development

## Prerequisites
Ensure you have the following installed:

- **Operating System**: Windows 10/11, macOS, or Linux
- **.NET SDK**: .NET 9.0 (latest stable version)
- **Database**: SQLite
- **IDE**: Visual Studio 2022 (latest version) or JetBrains Rider / VS Code
- **Git**: Version control system
- **Postman**: API testing tool

## Installation Steps

### 1. Download the Code Repository
```sh
git clone git@github.com:tianwen-zhou/LogisticsApi.git
cd LogisticsApi
```

### 2. Install .NET 9.0 SDK
Download and install the latest .NET 9.0 SDK from:
[.NET SDK Download](https://dotnet.microsoft.com/en-us/download)

Verify installation:
```sh
 dotnet --version
```

### 3. Set Up Development Environment
#### Windows
- Install **Visual Studio 2022** with the following workloads:
  - **ASP.NET and web development**
  - **.NET Core cross-platform development**

#### macOS & Linux
- Install **Visual Studio Code** or **JetBrains Rider**
- Install C# extension in VS Code (if applicable)

### 4. Setup Database (SQLite)
```sh
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```
Update `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=logistics.db"
}
```
Run migrations:
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Run the API
```sh
dotnet run
```

### 6. Test the API
Use Postman or curl to test the API endpoint:
```sh
curl http://localhost:5137/api/Drivers/1
```

### 7. Setup Version Control
```sh
git init
git add .
git commit -m "Initial commit"
git branch -M main
git remote add origin <your-repo-url>
git push -u origin main
```

## Additional Configurations
- **CORS Setup**: Modify `Program.cs` to allow cross-origin requests
- **Logging**: Configure logging in `appsettings.json`
- **Authentication**: Use JWT or OAuth2 for API security
- **Containerization**: Use Docker for deployment

This completes the setup for .NET 9.0 API development for the last-mile delivery platform. Happy coding!

