using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class CheckoutComplete
{
	private readonly GenericActions _genericActions;

	public CheckoutComplete(IPage page)
	{
		_genericActions = new GenericActions(page);
		CompleteContainer = page.Locator("[data-test='checkout-complete-container']");
		FinishedMessage = page.Locator("[data-test='complete-text']");
	}

	public ILocator CompleteContainer { get; }
	public ILocator FinishedMessage { get; }

	public bool waitForCompletePageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(CompleteContainer).GetAwaiter().GetResult();
	}

	public string getFinishedMessage()
	{
		if (!_genericActions.waitForElementToBeVisible(FinishedMessage).GetAwaiter().GetResult())
		{
			throw new PlaywrightException("Finished message is not visible on checkout complete page.");
		}

		return FinishedMessage.InnerTextAsync().GetAwaiter().GetResult();
	}
}
