# DemoShop Playwright C# Setup

This repository is configured with a Playwright test project in C#:

- `DemoShop.Playwright.Tests/DemoShop.Playwright.Tests.csproj`
- `DemoShop.Playwright.Tests/Tests/SmokeTests.cs`

## 1) Install .NET SDK (Linux)

Install .NET 8 SDK (example for Ubuntu):

```bash
sudo apt update
sudo apt install -y dotnet-sdk-8.0
```

Verify:

```bash
dotnet --version
```

## 2) Restore dependencies

```bash
cd DemoShop.Playwright.Tests
dotnet restore
```

## 3) Install Playwright browsers

```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
```

If `pwsh` is not installed:

```bash
sudo apt install -y powershell
```

## 4) Run tests

```bash
dotnet test
```

## Notes

- The sample test opens `https://example.com` and verifies the page title.
- Add more tests under `DemoShop.Playwright.Tests/Tests/`.
