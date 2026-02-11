using Reqnroll;
using PlaywrightReqnroll.Tests.Drivers;

namespace PlaywrightReqnroll.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly BrowserDriver _browserDriver;

    public Hooks(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        // Initialize the browser/page before each scenario
        await _browserDriver.GetPageAsync();
    }
}
