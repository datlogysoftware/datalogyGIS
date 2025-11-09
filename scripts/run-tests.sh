#!/bin/bash
# DatalogySoftware GIS Framework - Run All Tests

set -e

echo "================================"
echo "DatalogySoftware GIS Framework"
echo "Test Runner"
echo "================================"
echo ""

# Restore dependencies
echo "📦 Restoring dependencies..."
dotnet restore DatalogySoftware.Gis.sln

# Build
echo ""
echo "🔨 Building solution..."
dotnet build DatalogySoftware.Gis.sln --configuration Release --no-restore

# Run Core tests
echo ""
echo "🧪 Running Core tests..."
dotnet test tests/Datalogy.Gis.Core.Tests/Datalogy.Gis.Core.Tests.csproj \
  --configuration Release \
  --no-build \
  --verbosity normal \
  --logger "console;verbosity=detailed"

# Run Domain tests
echo ""
echo "🧪 Running Domain tests..."
dotnet test tests/Datalogy.Gis.Domain.Tests/Datalogy.Gis.Domain.Tests.csproj \
  --configuration Release \
  --no-build \
  --verbosity normal \
  --logger "console;verbosity=detailed"

# Run Sqlite integration tests
echo ""
echo "🧪 Running SQLite integration tests..."
dotnet test tests/Datalogy.Gis.Data.Sqlite.Tests/Datalogy.Gis.Data.Sqlite.Tests.csproj \
  --configuration Release \
  --no-build \
  --verbosity normal \
  --logger "console;verbosity=detailed"

echo ""
echo "✅ All tests passed!"
echo ""
echo "📊 Test Summary:"
echo "  - Core Tests: 14 tests"
echo "  - Domain Tests: 17 tests"
echo "  - Sqlite Tests: 12 tests"
echo "  - Total: 43+ tests"
echo ""
