using Reqnroll;
using PlaywrightReqnroll.Tests.Drivers;

namespace PlaywrightReqnroll.Tests.StepDefinitions;

[Binding]
public class NavigationSteps
{
    private readonly BrowserDriver _browserDriver;

    public NavigationSteps(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    [Given(@"I navigate to ""(.*)""")]
    public async Task GivenINavigateTo(string url)
    {
        var page = await _browserDriver.GetPageAsync();
        await page.GotoAsync(url);
    }

    [Then(@"I should see the page title contains ""(.*)""")]
    public async Task ThenIShouldSeeThePageTitleContains(string expectedTitle)
    {
        var page = await _browserDriver.GetPageAsync();
        var title = await page.TitleAsync();
        Assert.That(title, Does.Contain(expectedTitle));
    }

    [When(@"I click on a link with text ""(.*)""")]
    public async Task WhenIClickOnALinkWithText(string linkText)
    {
        var page = await _browserDriver.GetPageAsync();
        await page.GetByRole(Microsoft.Playwright.AriaRole.Link, new() { Name = linkText }).ClickAsync();
    }

    [Then(@"I should be navigated to a new page")]
    public async Task ThenIShouldBeNavigatedToANewPage()
    {
        var page = await _browserDriver.GetPageAsync();
        await page.WaitForLoadStateAsync();
        // Basic assertion that page loaded
        Assert.That(await page.TitleAsync(), Is.Not.Empty);
    }
}
