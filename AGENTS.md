# Agent Instructions for Playwright & Reqnroll Test Automation

## Overview

This repository demonstrates the integration of Playwright for browser automation with Reqnroll (SpecFlow successor) for behavior-driven development (BDD) testing in .NET.

## Purpose

The agent's role is to use Playwright to execute feature files and record those actions as implementation code/drivers for Reqnroll step definitions.

## How to Use This Setup

### 1. Understanding the Architecture

The project consists of:
- **Features Folder** (`Features/` at solution level): Contains `.feature` files written in Gherkin syntax
- **Step Definitions** (`PlaywrightReqnroll.Tests/StepDefinitions/`): C# implementations of Gherkin steps using Playwright
- **Drivers** (`PlaywrightReqnroll.Tests/Drivers/`): Browser driver abstraction using Playwright with screenshot support
- **Hooks** (`PlaywrightReqnroll.Tests/Hooks/`): Setup and teardown logic for scenarios, including automatic screenshot capture
- **Test Results** (`PlaywrightReqnroll.Tests/TestResults/`): Contains test results, coverage reports, and screenshots

### 2. Test Results and Diagnostics

**IMPORTANT:** Before running new tests, always check the previous test run results:

#### Check Test Results
1. **Look at TRX file** in `PlaywrightReqnroll.Tests/TestResults/` for the latest test run
2. **Review code coverage** in the coverage reports (Cobertura, JSON, LCOV formats available)
3. **Examine screenshots** in `PlaywrightReqnroll.Tests/TestResults/Screenshots/` for visual debugging

#### Running Tests
Only run new tests when:
- Explicitly asked to run tests
- Previous test results don't exist
- You've made code changes that need verification

**To run tests with coverage:**
```bash
cd PlaywrightReqnroll.Tests
dotnet test --settings test.runsettings --collect:"XPlat Code Coverage" --results-directory ./TestResults
```

**To view test results:**
- TRX files: Located in `TestResults/` directory
- Coverage: Look for `coverage.cobertura.xml` or `coverage.json`
- Screenshots: Located in `TestResults/Screenshots/`

### 3. Screenshot-Based Debugging

The test framework automatically captures screenshots:
- **After every step** for documentation and debugging
- **On test failure** with a "failure" suffix

#### Using Screenshots for Debugging
1. **Visual Diff Analysis**: Compare screenshots from different test runs to identify UI changes
2. **Step-by-Step Verification**: Review screenshots in sequence to understand test flow
3. **Failure Investigation**: Check the failure screenshot to see the exact state when the test failed
4. **Screenshot naming**: `{ScenarioName}_{StepText}_{Timestamp}.png` or `{ScenarioName}_failure_{Timestamp}.png`

#### Screenshot Best Practices
- Review screenshots in `TestResults/Screenshots/` before re-running tests
- Use screenshots to verify visual regressions
- Compare screenshots across test runs to identify unexpected changes
- Screenshots are full-page captures for complete context

### 4. Agent Workflow

When asked to implement test automation:

#### Step 1: Analyze Previous Results (DO THIS FIRST)
- **Check** `TestResults/` for latest test run results
- **Review** TRX files to understand what tests ran and their outcomes
- **Examine** code coverage reports to see what code is being tested
- **Look at** screenshots to understand current behavior

#### Step 2: Analyze Feature Requirements
- Read the feature file in `Features/` directory
- Understand the scenario and acceptance criteria
- Identify what browser actions need to be performed

#### Step 3: Execute with Playwright
- Use the Playwright MCP server configured in `.vscode/mcp.json`
- Manually interact with the web application to understand the flow
- Record the Playwright commands needed (e.g., `page.goto()`, `page.click()`, `page.fill()`)

#### Step 4: Generate Step Definitions
- Create or update step definition files in `StepDefinitions/`
- Use the `BrowserDriver` class to get the Playwright page instance
- Implement each Gherkin step with corresponding Playwright actions
- Add appropriate assertions using NUnit's `Assert.That()`

#### Step 5: Follow Coding Patterns

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

### 5. Best Practices

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

#### For Test Investigation:
- **ALWAYS check TestResults first** before running new tests
- Use screenshots for visual debugging instead of adding console logs
- Compare screenshots to identify visual regressions
- Review code coverage to ensure adequate test coverage
- Use TRX files to understand test execution patterns

### 6. Development Container

This project includes a dev container configuration:
- **Playwright browsers** are pre-installed via the devcontainer feature
- **Node.js** is available for Playwright tooling
- **.NET SDK** is configured for the project
- **VSCode extensions** for C#, Playwright, and Cucumber are pre-installed

To use:
1. Open the repository in VS Code
2. When prompted, click "Reopen in Container"
3. Wait for the container to build and setup script to complete

### 7. MCP Integration

The Playwright MCP server is configured in `.vscode/mcp.json` to provide:
- Browser automation capabilities via natural language
- Screenshot and recording capabilities
- Page inspection tools
- Network monitoring

Use the MCP server through your AI assistant to:
- Generate Playwright code from natural language descriptions
- Debug failing tests with live browser inspection
- Capture screenshots for visual verification

### 8. Creating New Tests

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
   dotnet test --settings test.runsettings --collect:"XPlat Code Coverage"
   ```

5. **Review results**:
   - Check TRX files for test outcomes
   - Review code coverage reports
   - Examine screenshots for visual verification

### 9. Debugging

- **Review screenshots first** in `TestResults/Screenshots/`
- Use `Headless = false` in `BrowserDriver.cs` to see browser (for interactive debugging only)
- Compare screenshots from failed vs. successful runs
- Check code coverage to identify untested code paths
- Review TRX files for detailed test execution information

### 10. Common Playwright Patterns

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
1. **Check previous test results** in TestResults/ directory before running new tests
2. **Review screenshots** for visual debugging and regression detection
3. **Analyze coverage reports** to ensure adequate test coverage
4. **Understand** the business requirements from feature files
5. **Execute** manual browser interactions using Playwright
6. **Record** those interactions as reusable step definitions
7. **Verify** the automation works correctly through test execution
8. **Use screenshots and test results** for debugging instead of watching console output

This creates a living documentation system where feature files describe behavior, step definitions provide executable validation, and test results/screenshots provide debugging insights.
