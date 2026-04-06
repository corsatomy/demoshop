namespace DemoShop.Playwright.Tests.Actions;

public static class StepsLogger
{
	public static void logStep(string stepName)
	{
		Console.WriteLine($"\n=== {stepName} ===\n");
	}

	public static void logStepFinished(string stepName)
	{
		Console.WriteLine($"\n✓ {stepName} - Finished\n");
	}
}
