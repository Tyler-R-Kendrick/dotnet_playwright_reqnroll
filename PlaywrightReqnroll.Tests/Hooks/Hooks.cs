using Reqnroll;
using PlaywrightReqnroll.Tests.Drivers;
using PlaywrightReqnroll.Tests.Support;

namespace PlaywrightReqnroll.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly BrowserDriver _browserDriver;
    private readonly TestScenarioContext _testScenarioContext;
    private readonly Reqnroll.ScenarioContext _reqnrollContext;

    public Hooks(BrowserDriver browserDriver, TestScenarioContext testScenarioContext, Reqnroll.ScenarioContext reqnrollContext)
    {
        _browserDriver = browserDriver;
        _testScenarioContext = testScenarioContext;
        _reqnrollContext = reqnrollContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        // Capture scenario information
        _testScenarioContext.ScenarioTitle = _reqnrollContext.ScenarioInfo.Title;
        _testScenarioContext.FeatureName = _reqnrollContext.ScenarioInfo.Title;
        _testScenarioContext.HasError = false;
        
        // Initialize the browser/page before each scenario
        await _browserDriver.GetPageAsync();
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        // Take screenshot if scenario failed
        if (_reqnrollContext.TestError != null)
        {
            _testScenarioContext.HasError = true;
            _testScenarioContext.Exception = _reqnrollContext.TestError;
            
            try
            {
                await _browserDriver.TakeScreenshotAsync(
                    _testScenarioContext.ScenarioTitle ?? "Unknown",
                    "failure"
                );
            }
            catch
            {
                // Ignore screenshot errors during cleanup
            }
        }
        
        // Clean up browser resources after each scenario
        await _browserDriver.DisposeAsync();
    }

    [AfterStep]
    public async Task AfterStep()
    {
        // Take screenshot after each step for debugging
        if (_reqnrollContext.StepContext.StepInfo != null)
        {
            var stepText = _reqnrollContext.StepContext.StepInfo.Text;
            await _browserDriver.TakeScreenshotAsync(
                _testScenarioContext.ScenarioTitle ?? "Unknown",
                stepText
            );
        }
    }
}
