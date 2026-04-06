using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class CartPage
{
	private readonly IPage _page;
	private readonly GenericActions _genericActions;

	public CartPage(IPage page)
	{
		_page = page;
		_genericActions = new GenericActions(page);
		CartList = page.Locator("[data-test='cart-list']");
		CheckoutButton = page.Locator("#checkout");
		CartItemName = page.Locator("[data-test='inventory-item-name']");
	}

	public ILocator CartList { get; }
	public ILocator CheckoutButton { get; }
	public ILocator CartItemName { get; }

	public bool waitForCartPageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(CartList).GetAwaiter().GetResult();
	}

	public bool checkForProductInTheList(string productName)
	{
		var cartItem = CartItemName.Filter(new() { HasTextString = productName });
		return _genericActions.waitForElementToBeVisible(cartItem).GetAwaiter().GetResult();
	}

	public CartPage clickCheckout()
	{
		_genericActions.clickOnElement(CheckoutButton).GetAwaiter().GetResult();
		return this;
	}
}
