#!/bin/bash
# DatalogySoftware GIS Framework - Build All Packages

set -e

echo "================================"
echo "DatalogySoftware GIS Framework"
echo "Build All Packages Script"
echo "================================"
echo ""

# Configuration
VERSION="${1:-3.0.0-preview}"
OUTPUT_DIR="./packages"
CONFIGURATION="Release"

echo "Version: $VERSION"
echo "Output: $OUTPUT_DIR"
echo "Configuration: $CONFIGURATION"
echo ""

# Clean output directory
echo "🧹 Cleaning output directory..."
rm -rf "$OUTPUT_DIR"
mkdir -p "$OUTPUT_DIR"

# Restore dependencies
echo ""
echo "📦 Restoring dependencies..."
dotnet restore DatalogySoftware.Gis.sln

# Build solution
echo ""
echo "🔨 Building solution..."
dotnet build DatalogySoftware.Gis.sln --configuration $CONFIGURATION --no-restore

# Pack Core packages
echo ""
echo "📦 Packing Core packages..."
dotnet pack src/Core/Datalogy.Gis.Core/Datalogy.Gis.Core.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

dotnet pack src/Core/Datalogy.Gis.Domain/Datalogy.Gis.Domain.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

dotnet pack src/Core/Datalogy.Gis.Application/Datalogy.Gis.Application.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

# Pack Data providers
echo ""
echo "📦 Packing Data providers..."
dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.Sqlite/Datalogy.Gis.Data.Sqlite.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.Postgres/Datalogy.Gis.Data.Postgres.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.CosmosDb/Datalogy.Gis.Data.CosmosDb.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

# Pack Rendering
echo ""
echo "📦 Packing Rendering packages..."
dotnet pack src/Rendering/Datalogy.Gis.Rendering.Core/Datalogy.Gis.Rendering.Core.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

dotnet pack src/Rendering/Datalogy.Gis.Rendering.SkiaSharp/Datalogy.Gis.Rendering.SkiaSharp.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

# Pack UI
echo ""
echo "📦 Packing UI packages..."
dotnet pack src/UI/Datalogy.Gis.Blazor/Datalogy.Gis.Blazor.csproj \
  -c $CONFIGURATION -o "$OUTPUT_DIR" /p:PackageVersion=$VERSION --no-build

echo ""
echo "✅ Build completed successfully!"
echo ""
echo "📊 Package Summary:"
ls -lh "$OUTPUT_DIR"/*.nupkg

echo ""
echo "🚀 Next steps:"
echo "  1. Test packages locally: dotnet nuget push $OUTPUT_DIR/*.nupkg --source local"
echo "  2. Publish to NuGet: ./scripts/publish-nuget.sh"
echo ""
