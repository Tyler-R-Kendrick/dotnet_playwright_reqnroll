#!/bin/bash
set -e

echo "Running post-create setup script..."

# Install Playwright browsers
echo "Installing Playwright browsers..."
npx playwright install --with-deps

# Install .NET Playwright
echo "Installing Microsoft.Playwright..."
dotnet tool install --global Microsoft.Playwright.CLI || dotnet tool update --global Microsoft.Playwright.CLI

# Restore .NET dependencies if solution exists
if compgen -G "*.sln" > /dev/null; then
    echo "Restoring .NET dependencies..."
    dotnet restore
fi

# Install agent-skills (placeholder for future configuration)
echo "Setting up agent-skills..."
# Agent-skills configuration will be handled via workspace settings

echo "Setup complete!"
