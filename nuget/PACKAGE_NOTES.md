# NuGet Package Publishing Guide

## DatalogySoftware GIS Framework - Package Publishing

### Prerequisites

1. **NuGet Account**
   - Create account at [nuget.org](https://www.nuget.org)
   - Generate API key from account settings

2. **Package Icon**
   - Located at: `nuget/icon.svg`
   - Convert to PNG (128x128) if needed for older NuGet versions

3. **README Files**
   - Each package should include the unified README
   - Located at: `nuget/README.md`

### Package Metadata

All packages inherit from `Directory.Build.props`:

```xml
<Company>DatalogySoftware</Company>
<Authors>DatalogySoftware</Authors>
<Product>DatalogySoftware GIS Framework</Product>
<Copyright>Copyright © 2024 DatalogySoftware</Copyright>
<PackageProjectUrl>https://www.datalogysoft.com/gis</PackageProjectUrl>
<RepositoryUrl>https://github.com/datalogysoft/gis-framework</RepositoryUrl>
<PackageTags>gis;geospatial;datalogysoft;datalogy;spatial;mapping</PackageTags>
```

### Manual Publishing

#### 1. Build Packages

```bash
# Build all packages
dotnet pack src/Core/Datalogy.Gis.Core/Datalogy.Gis.Core.csproj -c Release -o ./packages
dotnet pack src/Core/Datalogy.Gis.Domain/Datalogy.Gis.Domain.csproj -c Release -o ./packages
dotnet pack src/Core/Datalogy.Gis.Application/Datalogy.Gis.Application.csproj -c Release -o ./packages
dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.Sqlite/Datalogy.Gis.Data.Sqlite.csproj -c Release -o ./packages
dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.Postgres/Datalogy.Gis.Data.Postgres.csproj -c Release -o ./packages
dotnet pack src/Infrastructure/Data/Datalogy.Gis.Data.CosmosDb/Datalogy.Gis.Data.CosmosDb.csproj -c Release -o ./packages
dotnet pack src/Rendering/Datalogy.Gis.Rendering.Core/Datalogy.Gis.Rendering.Core.csproj -c Release -o ./packages
dotnet pack src/Rendering/Datalogy.Gis.Rendering.SkiaSharp/Datalogy.Gis.Rendering.SkiaSharp.csproj -c Release -o ./packages
dotnet pack src/UI/Datalogy.Gis.Blazor/Datalogy.Gis.Blazor.csproj -c Release -o ./packages
```

#### 2. Publish to NuGet

```bash
# Set API key (one time)
dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# Publish all packages
dotnet nuget push ./packages/*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

### Automated Publishing via GitHub Actions

The repository includes a GitHub Actions workflow for automated publishing:

#### Trigger on Tag

```bash
# Create and push a version tag
git tag v3.0.0-preview
git push origin v3.0.0-preview
```

This will:
1. Build all packages
2. Run tests
3. Create NuGet packages
4. Publish to NuGet.org (if secrets configured)
5. Create GitHub release with packages

#### Manual Workflow Trigger

1. Go to GitHub Actions
2. Select "Publish to NuGet" workflow
3. Click "Run workflow"
4. Enter version number (e.g., 3.0.0)

### Version Management

#### Version Format

- **Preview**: `3.0.0-preview`, `3.0.0-beta.1`
- **Release Candidate**: `3.0.0-rc.1`
- **Stable**: `3.0.0`
- **Patch**: `3.0.1`

#### Updating Version

Update in `Directory.Build.props`:

```xml
<VersionPrefix>3.0.0</VersionPrefix>
<VersionSuffix>preview</VersionSuffix>
```

Or specify during pack:

```bash
dotnet pack -c Release -o ./packages /p:Version=3.0.1
```

### Package Dependencies

Core packages have minimal dependencies:

```
Datalogy.Gis.Core
  └─ NetTopologySuite (2.5.0)

Datalogy.Gis.Domain
  └─ Datalogy.Gis.Core

Datalogy.Gis.Data.Sqlite
  ├─ Datalogy.Gis.Domain
  ├─ Microsoft.Data.Sqlite (8.0.0)
  └─ NetTopologySuite.IO.SpatiaLite (2.1.0)

Datalogy.Gis.Data.Postgres
  ├─ Datalogy.Gis.Domain
  ├─ Npgsql (8.0.0)
  └─ Npgsql.NetTopologySuite (8.0.0)
```

### GitHub Secrets Configuration

Add these secrets to your GitHub repository:

1. **NUGET_API_KEY**: Your NuGet.org API key

### Pre-Release Checklist

- [ ] All tests passing
- [ ] Version number updated
- [ ] README.md updated
- [ ] CHANGELOG.md updated
- [ ] Package icon present
- [ ] LICENSE file included
- [ ] Documentation complete

### Post-Release

1. **Verify on NuGet.org**
   - Check package appears correctly
   - Verify metadata and icon
   - Test installation

2. **Announce Release**
   - GitHub Discussions
   - Social media
   - Documentation site

3. **Monitor**
   - Watch for download statistics
   - Check for issues/feedback
   - Monitor GitHub issues

### Package URLs

After publishing, packages will be available at:

- Core: `https://nuget.org/packages/Datalogy.Gis.Core`
- Domain: `https://nuget.org/packages/Datalogy.Gis.Domain`
- Data.Sqlite: `https://nuget.org/packages/Datalogy.Gis.Data.Sqlite`
- Data.Postgres: `https://nuget.org/packages/Datalogy.Gis.Data.Postgres`
- Blazor: `https://nuget.org/packages/Datalogy.Gis.Blazor`

### Troubleshooting

**Issue**: Package upload fails
- Verify API key is correct
- Check version doesn't already exist
- Ensure package size is under 250MB

**Issue**: Icon not showing
- Convert SVG to PNG (128x128)
- Ensure icon is embedded in package
- Check file path in .csproj

**Issue**: Dependencies not resolving
- Verify package versions are published
- Check dependency version ranges
- Test with fresh NuGet cache

---

For questions or issues, contact: support@datalogysoft.com
