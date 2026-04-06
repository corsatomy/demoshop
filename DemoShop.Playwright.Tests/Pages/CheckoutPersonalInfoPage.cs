using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class CheckoutPersonalInfoPage
{
	private readonly GenericActions _genericActions;

	public CheckoutPersonalInfoPage(IPage page)
	{
		_genericActions = new GenericActions(page);
		CheckoutInfoContainer = page.Locator("[data-test='checkout-info-container']");
		NameField = page.Locator("[data-test='firstName']");
		SurnameField = page.Locator("[data-test='lastName']");
		ZipCodeField = page.Locator("[data-test='postalCode']");
		ContinueButton = page.Locator("[data-test='continue']");
	}

	public ILocator CheckoutInfoContainer { get; }
	public ILocator NameField { get; }
	public ILocator SurnameField { get; }
	public ILocator ZipCodeField { get; }
	public ILocator ContinueButton { get; }

	public bool waitForCheckoutPersonalInfoPageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(CheckoutInfoContainer).GetAwaiter().GetResult();
	}

	public CheckoutPersonalInfoPage setClientInfo(string name, string surname, string zipCode)
	{
		setName(name);
		setSurname(surname);
		setZipCode(zipCode);
		clickContinue();
		return this;
	}

	private CheckoutPersonalInfoPage setName(string name)
	{
		_genericActions.setFieldText(NameField, name).GetAwaiter().GetResult();
		return this;
	}

	private CheckoutPersonalInfoPage setSurname(string surname)
	{
		_genericActions.setFieldText(SurnameField, surname).GetAwaiter().GetResult();
		return this;
	}

	private CheckoutPersonalInfoPage setZipCode(string zipCode)
	{
		_genericActions.setFieldText(ZipCodeField, zipCode).GetAwaiter().GetResult();
		return this;
	}

	private CheckoutPersonalInfoPage clickContinue()
	{
		_genericActions.clickOnElement(ContinueButton).GetAwaiter().GetResult();
		return this;
	}
}
