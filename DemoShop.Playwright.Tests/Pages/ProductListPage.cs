using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class ProductListPage
{
	private readonly GenericActions _genericActions;

	public ProductListPage(IPage page)
	{
		_genericActions = new GenericActions(page);
		InventoryContainer = page.Locator("[data-test='inventory-container']");
		InventoryItem = page.Locator("[data-test='inventory-item']");
		CartLink = page.Locator("[data-test='shopping-cart-link']");
		CartBadge = page.Locator("[data-test='shopping-cart-badge']");
		ProductName = page.Locator("[data-test='inventory-item-name']");
	}

	public ILocator InventoryContainer { get; }
	public ILocator InventoryItem { get; }
	public ILocator CartLink { get; }
	public ILocator CartBadge { get; }
	public ILocator ProductName { get; }

	public bool waitForProductListPageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(InventoryContainer).GetAwaiter().GetResult();
	}

	public ProductListPage addProductToCartByName(string productName)
	{
		var productNameLocator = findElementByText(ProductName, productName);
		var productCard = InventoryItem.Filter(new() { Has = productNameLocator });
		var addToCartButton = findElementByText(productCard.Locator("button"), "Add to cart");
		_genericActions.clickOnElement(addToCartButton).GetAwaiter().GetResult();
		return this;
	}

	private ILocator findElementByText(ILocator locator, string text)
	{
		var textLocator = locator.Filter(new() { HasTextString = text });
		if (!_genericActions.waitForElementToBeVisible(textLocator).GetAwaiter().GetResult())
		{
			throw new PlaywrightException($"Element with text '{text}' was not found.");
		}

		return textLocator;
	}

	public ProductListPage openCart()
	{
		_genericActions.clickOnElement(CartLink).GetAwaiter().GetResult();
		return this;
	}

	public bool waitForCartBadge()
	{
		return _genericActions.waitForElementToBeVisible(CartBadge).GetAwaiter().GetResult();
	}
}
