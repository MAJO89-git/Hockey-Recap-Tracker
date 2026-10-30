# Hockey-Recap-Tracker


## Formatting

Run these commands from the repository root.

### Apply formatting

```powershell
dotnet format whitespace
dotnet format whitespace tests/HockeyRecapTracker.Tests/HockeyRecapTracker.Tests.csproj
```

### Check formatting without modifying files

```powershell
dotnet format whitespace --verify-no-changes
dotnet format whitespace tests/HockeyRecapTracker.Tests/HockeyRecapTracker.Tests.csproj --verify-no-changes
```

These commands follow the rules in `.editorconfig`.
The first command in each pair targets the main project; the second targets the test project.