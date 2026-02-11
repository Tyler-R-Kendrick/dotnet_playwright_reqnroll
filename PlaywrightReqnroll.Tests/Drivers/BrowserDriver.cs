using Microsoft.Playwright;

namespace PlaywrightReqnroll.Tests.Drivers;

public class BrowserDriver : IDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;

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

    public void Dispose()
    {
        _page?.CloseAsync().GetAwaiter().GetResult();
        _context?.CloseAsync().GetAwaiter().GetResult();
        _browser?.CloseAsync().GetAwaiter().GetResult();
        _playwright?.Dispose();
    }
}
