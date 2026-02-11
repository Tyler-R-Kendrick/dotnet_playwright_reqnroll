# Agent Instructions for Playwright & Reqnroll Test Automation

## Overview

This repository demonstrates the integration of Playwright for browser automation with Reqnroll (SpecFlow successor) for behavior-driven development (BDD) testing in .NET.

## Purpose

The agent's role is to use Playwright to execute feature files and record those actions as implementation code/drivers for Reqnroll step definitions.

## How to Use This Setup

### 1. Understanding the Architecture

The project consists of:
- **Features Folder** (`PlaywrightReqnroll.Tests/Features/`): Contains `.feature` files written in Gherkin syntax
- **Step Definitions** (`PlaywrightReqnroll.Tests/StepDefinitions/`): C# implementations of Gherkin steps using Playwright
- **Drivers** (`PlaywrightReqnroll.Tests/Drivers/`): Browser driver abstraction using Playwright
- **Hooks** (`PlaywrightReqnroll.Tests/Hooks/`): Setup and teardown logic for scenarios

### 2. Agent Workflow

When asked to implement test automation:

#### Step 1: Analyze Feature Requirements
- Read the feature file in `Features/` directory
- Understand the scenario and acceptance criteria
- Identify what browser actions need to be performed

#### Step 2: Execute with Playwright
- Use the Playwright MCP server configured in `.vscode/mcp.json`
- Manually interact with the web application to understand the flow
- Record the Playwright commands needed (e.g., `page.goto()`, `page.click()`, `page.fill()`)

#### Step 3: Generate Step Definitions
- Create or update step definition files in `StepDefinitions/`
- Use the `BrowserDriver` class to get the Playwright page instance
- Implement each Gherkin step with corresponding Playwright actions
- Add appropriate assertions using NUnit's `Assert.That()`

#### Step 4: Follow Coding Patterns

**Example Step Definition:**
```csharp
[Given(@"I navigate to ""(.*)""")]
public async Task GivenINavigateTo(string url)
{
    var page = await _browserDriver.GetPageAsync();
    await page.GotoAsync(url);
}

[When(@"I click on ""(.*)""")]
public async Task WhenIClickOn(string elementText)
{
    var page = await _browserDriver.GetPageAsync();
    await page.GetByText(elementText).ClickAsync();
}

[Then(@"I should see ""(.*)""")]
public async Task ThenIShouldSee(string expectedText)
{
    var page = await _browserDriver.GetPageAsync();
    var content = await page.ContentAsync();
    Assert.That(content, Does.Contain(expectedText));
}
```

### 3. Running Tests

Execute tests using:
```bash
dotnet test
```

Or for specific features:
```bash
dotnet test --filter "FullyQualifiedName~ExampleNavigation"
```

### 4. Best Practices

#### For Feature Files:
- Use clear, business-readable language
- Follow Given-When-Then structure
- Keep scenarios focused and independent
- Use scenario outlines for data-driven tests

#### For Step Definitions:
- Reuse steps across multiple scenarios
- Keep steps atomic and single-purpose
- Use dependency injection for the BrowserDriver
- All methods should be async and return Task
- Use Playwright's built-in waiting mechanisms

#### For Playwright Selectors:
- Prefer `GetByRole()`, `GetByLabel()`, `GetByText()` over CSS selectors
- Use `GetByTestId()` for stable test-specific attributes
- Avoid XPath when possible
- Chain locators for specificity: `page.GetByRole(AriaRole.Button).Filter(new() { HasText = "Submit" })`

### 5. Development Container

This project includes a dev container configuration:
- **Playwright browsers** are pre-installed via the devcontainer feature
- **Node.js** is available for Playwright tooling
- **.NET SDK** is configured for the project
- **VSCode extensions** for C#, Playwright, and Cucumber are pre-installed

To use:
1. Open the repository in VS Code
2. When prompted, click "Reopen in Container"
3. Wait for the container to build and setup script to complete

### 6. MCP Integration

The Playwright MCP server is configured in `.vscode/mcp.json` to provide:
- Browser automation capabilities via natural language
- Screenshot and recording capabilities
- Page inspection tools
- Network monitoring

Use the MCP server through your AI assistant to:
- Generate Playwright code from natural language descriptions
- Debug failing tests with live browser inspection
- Capture screenshots for visual verification

### 7. Creating New Tests

To add a new test scenario:

1. **Create a feature file** in `Features/`:
   ```gherkin
   Feature: Login functionality
     Scenario: Successful login
       Given I am on the login page
       When I enter valid credentials
       Then I should be logged in
   ```

2. **Generate step definition skeleton**:
   - Build the project to see missing step definitions
   - Use Reqnroll's code generation hints
   - Or manually create in `StepDefinitions/`

3. **Implement steps with Playwright**:
   - Use `_browserDriver.GetPageAsync()` to get the page
   - Write Playwright automation code
   - Add assertions for verification

4. **Run and iterate**:
   ```bash
   dotnet test
   ```

### 8. Debugging

- Use `Headless = false` in `BrowserDriver.cs` to see browser
- Add `await page.PauseAsync()` to pause execution
- Use `await page.ScreenshotAsync()` to capture state
- Enable trace: `await context.Tracing.StartAsync()`

### 9. Common Playwright Patterns

**Navigation:**
```csharp
await page.GotoAsync("https://example.com");
await page.WaitForLoadStateAsync();
```

**Element Interaction:**
```csharp
await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
await page.GetByLabel("Username").FillAsync("user@example.com");
```

**Assertions:**
```csharp
await Expect(page.GetByText("Welcome")).ToBeVisibleAsync();
Assert.That(await page.TitleAsync(), Is.EqualTo("Dashboard"));
```

**Waiting:**
```csharp
await page.WaitForSelectorAsync("text=Loading complete");
await page.WaitForURLAsync("**/dashboard");
```

## Summary

As an agent, your goal is to:
1. **Understand** the business requirements from feature files
2. **Execute** manual browser interactions using Playwright
3. **Record** those interactions as reusable step definitions
4. **Verify** the automation works correctly through test execution

This creates a living documentation system where feature files describe behavior and step definitions provide executable validation.
