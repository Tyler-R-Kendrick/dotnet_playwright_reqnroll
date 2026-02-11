using Reqnroll;
using PlaywrightReqnroll.Tests.Drivers;

namespace PlaywrightReqnroll.Tests.StepDefinitions;

[Binding]
public class LocalTestSteps
{
    private readonly BrowserDriver _browserDriver;

    public LocalTestSteps(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    [Given(@"I navigate to a page with HTML content")]
    public async Task GivenINavigateToAPageWithHTMLContent()
    {
        var page = await _browserDriver.GetPageAsync();
        var html = @"
            <!DOCTYPE html>
            <html>
            <head><title>Test Page</title></head>
            <body>
                <h1>Hello World</h1>
                <p>This is a test page</p>
            </body>
            </html>
        ";
        await page.SetContentAsync(html);
    }

    [Then(@"I should see ""(.*)"" on the page")]
    public async Task ThenIShouldSeeOnThePage(string expectedText)
    {
        var page = await _browserDriver.GetPageAsync();
        var content = await page.ContentAsync();
        Assert.That(content, Does.Contain(expectedText));
    }
}
