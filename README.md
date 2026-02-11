# dotnet_playwright_reqnroll

An example of Playwright and Reqnroll used together to automate test generation and execution.

## Overview

This repository demonstrates a complete dev container setup for browser automation testing using:
- **Playwright** for browser automation
- **Reqnroll** (SpecFlow successor) for BDD testing
- **.NET 8** as the runtime platform
- **NUnit** as the test framework

## Getting Started

### Prerequisites

- Docker Desktop (for dev container support)
- Visual Studio Code with Remote - Containers extension

### Quick Start

1. **Open in Dev Container**
   - Open this repository in VS Code
   - When prompted, click "Reopen in Container"
   - Wait for the container to build and setup to complete

2. **Install Playwright Browsers** (if not auto-installed)
   ```bash
   npx playwright install --with-deps
   ```

3. **Build the Solution**
   ```bash
   dotnet build
   ```

4. **Run Tests**
   ```bash
   dotnet test
   ```

## Project Structure

```
.
├── .devcontainer/
│   ├── devcontainer.json      # Dev container configuration
│   └── setup.sh               # Post-create setup script
├── .vscode/
│   └── mcp.json               # Playwright MCP server configuration
├── PlaywrightReqnroll.Tests/
│   ├── Features/              # Gherkin feature files
│   ├── StepDefinitions/       # Step definition implementations
│   ├── Drivers/               # Browser driver abstraction
│   ├── Hooks/                 # Test hooks (setup/teardown)
│   └── reqnroll.json          # Reqnroll configuration
├── AGENTS.md                  # Agent instructions for test automation
└── PlaywrightReqnroll.sln     # .NET solution file
```

## Features

### Dev Container
- Pre-configured with .NET 8 SDK
- Playwright feature pre-installed
- Node.js 20 for Playwright tooling
- VS Code extensions for C#, Playwright, and Cucumber

### Playwright Integration
- Browser driver abstraction layer
- Support for Chromium, Firefox, and WebKit
- Configured for headless execution
- MCP server for AI-assisted test creation

### Reqnroll (BDD)
- Feature files in Gherkin syntax
- NUnit integration
- Step definition auto-generation support
- Comprehensive test reporting

## Writing Tests

### 1. Create a Feature File

Create a `.feature` file in `PlaywrightReqnroll.Tests/Features/`:

```gherkin
Feature: Login functionality
  Scenario: Successful login
    Given I navigate to "https://example.com/login"
    When I enter username "user@example.com"
    And I enter password "password123"
    And I click the login button
    Then I should see "Welcome back"
```

### 2. Implement Step Definitions

Create step definitions in `PlaywrightReqnroll.Tests/StepDefinitions/`:

```csharp
[Binding]
public class LoginSteps
{
    private readonly BrowserDriver _browserDriver;

    public LoginSteps(BrowserDriver browserDriver)
    {
        _browserDriver = browserDriver;
    }

    [When(@"I enter username ""(.*)""")]
    public async Task WhenIEnterUsername(string username)
    {
        var page = await _browserDriver.GetPageAsync();
        await page.GetByLabel("Username").FillAsync(username);
    }
}
```

### 3. Run Your Tests

```bash
dotnet test
```

## Agent Workflow

See [AGENTS.md](AGENTS.md) for detailed instructions on using AI agents to:
- Execute feature files with Playwright
- Record browser interactions
- Generate step definitions automatically
- Debug and troubleshoot tests

## Configuration

### Playwright Settings

Modify `PlaywrightReqnroll.Tests/Drivers/BrowserDriver.cs` to customize:
- Browser type (Chromium, Firefox, WebKit)
- Headless mode
- Viewport size
- Device emulation

### Reqnroll Settings

Configure in `PlaywrightReqnroll.Tests/reqnroll.json`:
- Language settings
- Trace options
- Plugin configurations

## CI/CD Integration

The tests can be integrated into CI/CD pipelines:

```yaml
# Example GitHub Actions workflow
- name: Install dependencies
  run: |
    dotnet restore
    npx playwright install --with-deps

- name: Run tests
  run: dotnet test
```

## Resources

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [Reqnroll Documentation](https://docs.reqnroll.net/)
- [NUnit Documentation](https://docs.nunit.org/)

## License

See [LICENSE](LICENSE) file for details.
