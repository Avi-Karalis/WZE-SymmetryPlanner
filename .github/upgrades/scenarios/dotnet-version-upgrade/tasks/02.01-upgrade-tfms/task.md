## Update projects targeting net8.0 to net10.0

### Objective
Update TargetFramework values from `net8.0` to `net10.0` for projects that still target .NET 8.

### Steps
1. Locate .csproj files with `<TargetFramework>net8.0</TargetFramework>`.
2. Update to `<TargetFramework>net10.0</TargetFramework>`.
3. Run `dotnet restore` and `dotnet build` to validate.
4. Record changes in `progress-detail.md` and commit.

### Validation
- Solution builds successfully targeting `net10.0` with no errors.
