#!/bin/bash
# DatalogySoftware GIS Framework - Publish to NuGet

set -e

echo "================================"
echo "DatalogySoftware GIS Framework"
echo "Publish to NuGet Script"
echo "================================"
echo ""

# Check for API key
if [ -z "$NUGET_API_KEY" ]; then
  echo "❌ Error: NUGET_API_KEY environment variable not set"
  echo ""
  echo "Usage:"
  echo "  export NUGET_API_KEY='your-api-key'"
  echo "  ./scripts/publish-nuget.sh"
  echo ""
  exit 1
fi

PACKAGES_DIR="./packages"
NUGET_SOURCE="https://api.nuget.org/v3/index.json"

# Check if packages exist
if [ ! -d "$PACKAGES_DIR" ] || [ -z "$(ls -A $PACKAGES_DIR/*.nupkg 2>/dev/null)" ]; then
  echo "❌ Error: No packages found in $PACKAGES_DIR"
  echo ""
  echo "Run ./scripts/build-all.sh first"
  echo ""
  exit 1
fi

echo "📦 Packages to publish:"
ls -1 "$PACKAGES_DIR"/*.nupkg
echo ""

read -p "Continue with publishing? (y/N) " -n 1 -r
echo ""
if [[ ! $REPLY =~ ^[Yy]$ ]]; then
  echo "Cancelled."
  exit 0
fi

echo ""
echo "🚀 Publishing to NuGet.org..."
echo ""

dotnet nuget push "$PACKAGES_DIR/*.nupkg" \
  --api-key "$NUGET_API_KEY" \
  --source "$NUGET_SOURCE" \
  --skip-duplicate

echo ""
echo "✅ Packages published successfully!"
echo ""
echo "🔗 View packages at:"
echo "  https://nuget.org/packages/Datalogy.Gis.Core"
echo ""
echo "⏱️  Note: It may take a few minutes for packages to appear in search"
echo ""
