using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class CheckoutOverviewPage
{
	private readonly IPage _page;
	private readonly GenericActions _genericActions;

	public CheckoutOverviewPage(IPage page)
	{
		_page = page;
		_genericActions = new GenericActions(page);
		OverviewContainer = page.Locator("[data-test='checkout-summary-container']");
		FinishButton = page.Locator("#finish");
		ItemName = page.Locator("[data-test='inventory-item-name']");
	}

	public ILocator OverviewContainer { get; }
	public ILocator FinishButton { get; }
	public ILocator ItemName { get; }

	public bool waitCheckoutOverviewPageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(OverviewContainer).GetAwaiter().GetResult();
	}

	public bool checkSelectedItemsList(params string[] itemNames)
	{
		foreach (var itemName in itemNames)
		{
			var itemLocator = ItemName.Filter(new() { HasTextString = itemName });
			if (!_genericActions.waitForElementToBeVisible(itemLocator).GetAwaiter().GetResult())
			{
				return false;
			}
		}

		return true;
	}

	public CheckoutOverviewPage pressFinish()
	{
		_genericActions.clickOnElement(FinishButton).GetAwaiter().GetResult();
		return this;
	}
}
