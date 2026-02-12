#!/bin/bash
set -e

echo "Running post-create setup script..."

# Restore .NET dependencies if solution exists
dotnet restore

# Install Playwright agent skills
echo "Installing Playwright agent skills..."
npx -y -g skills add microsoft/playwright --skill playwright-api -a github-copilot -y
npx -y -g skills add microsoft/playwright-cli --skill playwright-cli -a github-copilot -y

# Install Playwright browsers for .NET
pwsh PlaywrightReqnroll.Tests/bin/Debug/net8.0/playwright.ps1 install

# Install playwright-cli (if not already installed)
npm install -g playwright-cli

# Install the CLI browsers (headless supported by default)
npx -y -g playwright install --with-deps
echo "Setup complete!"
