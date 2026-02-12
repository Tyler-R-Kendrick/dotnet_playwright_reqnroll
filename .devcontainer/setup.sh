#!/bin/bash
set -e

echo "Running post-create setup script..."

# Restore .NET dependencies if solution exists
dotnet restore

# Install Playwright agent skills
echo "Installing Playwright agent skills..."
npx -y -g skills add microsoft/playwright --skill playwright-api -a github-copilot -y
npx -y -g skills add microsoft/playwright-cli --skill playwright-cli -a github-copilot -y

npm install -g playwright
# Install Playwright CLI
npm install -g playwright-cli

# Install the Playwright browsers (headless supported by default)
npx -y -g playwright install chrome --with-deps
echo "Setup complete!"
