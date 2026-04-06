using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using DemoShop.Playwright.Tests.Pages;
using DemoShop.Playwright.Tests.Actions;
using DemoShop.Playwright.Tests.Config;
using DemoShop.Playwright.Tests.TestData;
using static DemoShop.Playwright.Tests.Actions.StepsLogger;

namespace DemoShop.Playwright.Tests.Tests;

public class SmokeTests : PageTest
{
    [Test]
    public async Task purchaseTShirtTest()
    {
        var baseUrl = UrlSettings.BaseUrl;
        var clientInfo = ClientInfo.Default;
        
        // Retrieve credentials from environment variables (set by CI/CD pipeline)
        // Falls back to default values if not provided
        var username = Environment.GetEnvironmentVariable("TEST_USERNAME") ?? "standard_user";
        var password = Environment.GetEnvironmentVariable("TEST_PASSWORD") ?? "secret_sauce";

        var loginPage = new LoginPage(Page);
        var productListPage = new ProductListPage(Page);
        var cartPage = new CartPage(Page);
        var checkoutPersonalInfoPage = new CheckoutPersonalInfoPage(Page);
        var checkoutOverviewPage = new CheckoutOverviewPage(Page);
        var checkoutCompletePage = new CheckoutComplete(Page);
        var genericActions = new GenericActions(Page);

        logStep("Navigate to Sauce Demo page");
        await genericActions.openUrl(baseUrl);
        Assert.That(loginPage.waitForLoginPageLoaded(), Is.True, "Login page did not load in time.");
        logStepFinished("Navigate to Sauce Demo page");

        logStep("Login with provided credentials");
        loginPage.loginToSystem(
            username: username,
            password: password);
        Assert.That(productListPage.waitForProductListPageLoaded(), Is.True, "Product list page did not load.");
        logStepFinished("Login with standard_user credentials");

        logStep("Add Sauce Labs Bolt T-Shirt to cart");
        productListPage.addProductToCartByName("Sauce Labs Bolt T-Shirt");
        Assert.That(productListPage.waitForCartBadge(), Is.True, "Shopping cart badge is not visible.");
        logStepFinished("Add Sauce Labs Bolt T-Shirt to cart");

        logStep("View shopping cart");
        productListPage.openCart();
        Assert.Multiple(() =>
        {
            Assert.That(cartPage.waitForCartPageLoaded(), Is.True, "Cart page did not load.");
            Assert.That(cartPage.checkForProductInTheList("Sauce Labs Bolt T-Shirt"), Is.True, "Selected item is not visible in cart.");
        });
        logStepFinished("View shopping cart");

        logStep("Proceed to checkout");
        cartPage.clickCheckout();
        Assert.That(checkoutPersonalInfoPage.waitForCheckoutPersonalInfoPageLoaded(), Is.True, "Checkout personal info page did not load.");
        logStepFinished("Proceed to checkout");

        logStep("Fill personal info");
        checkoutPersonalInfoPage.setClientInfo(
            name: clientInfo.Name,
            surname: clientInfo.Surname,
            zipCode: clientInfo.ZipCode);
        logStepFinished("Fill personal info");

        logStep("Finish payment");
        Assert.Multiple(() =>
        {
            Assert.That(checkoutOverviewPage.waitCheckoutOverviewPageLoaded(), Is.True, "Checkout overview page did not load.");
            Assert.That(checkoutOverviewPage.checkSelectedItemsList("Sauce Labs Bolt T-Shirt"), Is.True, "Selected items are missing in checkout overview.");
        });
        checkoutOverviewPage.pressFinish();

        Assert.Multiple(() =>
        {
            Assert.That(checkoutCompletePage.waitForCompletePageLoaded(), Is.True, "Checkout complete page did not load.");
            Assert.That(
                checkoutCompletePage.getFinishedMessage().Trim(),
                Is.EqualTo("Your order has been dispatched, and will arrive just as fast as the pony can get there!")
            );
        });
        logStepFinished("Finish payment");
    }
}
