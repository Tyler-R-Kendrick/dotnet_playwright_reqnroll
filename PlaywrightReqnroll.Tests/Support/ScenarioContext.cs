namespace PlaywrightReqnroll.Tests.Support;

public class TestScenarioContext
{
    public string? ScenarioTitle { get; set; }
    public string? FeatureName { get; set; }
    public bool HasError { get; set; }
    public Exception? Exception { get; set; }
}
