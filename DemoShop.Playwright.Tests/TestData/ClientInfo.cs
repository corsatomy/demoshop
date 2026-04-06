namespace DemoShop.Playwright.Tests.TestData;

public record ClientInfo(string Name, string Surname, string ZipCode)
{
	public static readonly ClientInfo Default = new("Artjoms", "Omelcuks", "LV-1009");
}
