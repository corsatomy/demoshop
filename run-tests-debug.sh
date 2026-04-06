#!/bin/bash
# Run Playwright tests with debugger (headless=false)
# Usage: ./run-tests-debug.sh

cd "$(dirname "$0")"

# Check if HEADED environment variable is already set
if [ -z "$HEADED" ]; then
    export HEADED=true
fi

echo "Running tests with browser visible (HEADED mode)..."
echo "To run in headless mode, use: dotnet test DemoShop.Playwright.Tests/DemoShop.Playwright.Tests.csproj"
echo ""

dotnet test DemoShop.Playwright.Tests/DemoShop.Playwright.Tests.csproj --verbosity normal
