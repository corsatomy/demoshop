using Microsoft.Playwright;

namespace DemoShop.Playwright.Tests.Actions;

public class GenericActions
{
	private readonly IPage _page;

	public GenericActions(IPage page)
	{
		_page = page;
	}

	public async Task openUrl(string url)
	{
		await _page.GotoAsync(url);
	}

	public async Task clickOnElement(ILocator locator)
	{
		await locator.ClickAsync();
	}

	public async Task setFieldText(ILocator locator, string text)
	{
		await locator.FillAsync(text);
	}

	public async Task<bool> waitForElementToBeVisible(ILocator locator)
	{
		try
		{
			await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
			return true;
		}
		catch (TimeoutException)
		{
			return false;
		}
		catch (PlaywrightException ex) when (ex.Message.Contains("Timeout", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}
	}
}
