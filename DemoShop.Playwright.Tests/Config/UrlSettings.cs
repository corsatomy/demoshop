using System.Text.Json;

namespace DemoShop.Playwright.Tests.Config;

public static class UrlSettings
{
    public static string BaseUrl => LoadBaseUrl();

    private static string LoadBaseUrl()
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Config file not found: {configPath}");
        }

        using var stream = File.OpenRead(configPath);
        using var document = JsonDocument.Parse(stream);

        if (!document.RootElement.TryGetProperty("TestSettings", out var settingsSection) ||
            !settingsSection.TryGetProperty("BaseUrl", out var urlElement))
        {
            throw new InvalidOperationException("Missing TestSettings:BaseUrl in appsettings.json");
        }

        var url = urlElement.GetString();
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new InvalidOperationException("TestSettings:BaseUrl cannot be empty");
        }

        return url;
    }
}
