using Microsoft.Playwright;

namespace PlaywrightReqnroll.Tests.Drivers;

public class BrowserDriver : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;
    private readonly string _screenshotPath;

    public BrowserDriver()
    {
        // Find the project directory by looking for the .csproj file
        var currentDir = Directory.GetCurrentDirectory();
        var projectDir = FindProjectDirectory(currentDir) ?? currentDir;
        _screenshotPath = Path.Combine(projectDir, "TestResults", "Screenshots");
        Directory.CreateDirectory(_screenshotPath);
    }

    private static string? FindProjectDirectory(string startDir)
    {
        var dir = new DirectoryInfo(startDir);
        while (dir != null)
        {
            if (dir.GetFiles("*.csproj").Length > 0)
                return dir.FullName;
            dir = dir.Parent;
        }
        return null;
    }

    public async Task<IPage> GetPageAsync()
    {
        if (_page != null)
            return _page;

        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();

        return _page;
    }

    public async Task TakeScreenshotAsync(string scenarioName, string stepName = "")
    {
        if (_page == null)
            return;

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var sanitizedScenario = SanitizeFileName(scenarioName);
        var sanitizedStep = string.IsNullOrEmpty(stepName) ? "" : $"_{SanitizeFileName(stepName)}";
        var fileName = $"{sanitizedScenario}{sanitizedStep}_{timestamp}.png";
        var filePath = Path.Combine(_screenshotPath, fileName);

        await _page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = filePath,
            FullPage = true
        });
    }

    private static string SanitizeFileName(string fileName)
    {
        var invalid = Path.GetInvalidFileNameChars();
        return string.Join("_", fileName.Split(invalid, StringSplitOptions.RemoveEmptyEntries));
    }

    public async ValueTask DisposeAsync()
    {
        if (_page != null)
        {
            await _page.CloseAsync();
            _page = null;
        }
        
        if (_context != null)
        {
            await _context.CloseAsync();
            _context = null;
        }
        
        if (_browser != null)
        {
            await _browser.CloseAsync();
            _browser = null;
        }
        
        if (_playwright != null)
        {
            _playwright.Dispose();
            _playwright = null;
        }
    }
}
