using Microsoft.Playwright;
using DemoShop.Playwright.Tests.Actions;

namespace DemoShop.Playwright.Tests.Pages;

public class LoginPage
{
	private readonly GenericActions _genericActions;

	public LoginPage(IPage page)
	{
		_genericActions = new GenericActions(page);
		UsernameField = page.Locator("[data-test='username']");
		PasswordField = page.Locator("[data-test='password']");
		LoginButton = page.Locator("#login-button");
	}

	public ILocator UsernameField { get; }
	public ILocator PasswordField { get; }
	public ILocator LoginButton { get; }

	public bool waitForLoginPageLoaded()
	{
		return _genericActions.waitForElementToBeVisible(UsernameField).GetAwaiter().GetResult();
	}

	public LoginPage loginToSystem(string username, string password)
	{
		setUsername(username);
		setPassword(password);
		clickLogin();
		return this;
	}

	private LoginPage setUsername(string username)
	{
		_genericActions.setFieldText(UsernameField, username).GetAwaiter().GetResult();
		return this;
	}

	private LoginPage setPassword(string password)
	{
		_genericActions.setFieldText(PasswordField, password).GetAwaiter().GetResult();
		return this;
	}

	private LoginPage clickLogin()
	{
		_genericActions.clickOnElement(LoginButton).GetAwaiter().GetResult();
		return this;
	}
}
