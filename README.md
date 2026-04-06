# Fitness Tracker (WPF)

A modern .NET 8 WPF desktop app for fitness and health tracking, with separate user/admin experiences, dashboard analytics, and in-memory demo data.

## Features

- Login screen with validation and demo credentials
- User area:
  - Dashboard summary
  - Meals log
  - Exercise log
  - Health metrics
  - Goals
  - Profile view
- Admin area:
  - Admin dashboard with monthly activity chart
  - Manage users (search, ID filter, add, edit, delete)
- Responsive-style layout behavior in key views
- Material Design styling and iconography

## Tech Stack

- .NET 8 (`net8.0-windows`)
- WPF
- MVVM pattern
- `MaterialDesignThemes` + `MaterialDesignColors`
- `LiveChartsCore.SkiaSharpView.WPF` for charting

## Project Structure

```text
Project1/
├── App.xaml
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── FitnessTracker.csproj
├── Data/
│   └── SampleDataStore.cs
├── Models/
├── ViewModels/
├── Views/
├── Converters/
└── Helpers/
```

## Demo Credentials

Use the following on the login page:

- User: `demo` / `password`
- Admin: `admin` / `password`, `admin`, or `!`

## Getting Started

### Prerequisites

- .NET 8 SDK
- Windows (WPF desktop support)

### Run

```bash
dotnet restore
dotnet run --project FitnessTracker.csproj
```

### Build

```bash
dotnet build FitnessTracker.csproj -c Release
```

## Architecture Notes

- App navigation is hosted in `MainWindow` and switches user controls via view models.
- Data is currently in-memory (`Data/SampleDataStore.cs`) for demo/testing.
- Commands are implemented via `RelayCommand`.
- Most screens bind to observable collections and update live after CRUD operations.

## Known Notes

- You may see `NU1701` warnings from transitive Skia/OpenTK packages when restoring/building.  
  The app still builds and runs successfully.

## Future Improvements

- Replace in-memory store with persistent storage (SQLite / EF Core / API)
- Add unit tests for view models and filtering logic
- Add authentication/authorization backed by secure user storage
