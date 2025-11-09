# Changelog

All notable changes to DatalogySoftware GIS Framework will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0-preview] - 2025-11-09

### Added
- Complete framework rebrand under **DatalogySoftware** brand identity
- New namespace pattern: `Datalogy.Gis.*` for all packages
- **Core Libraries**:
  - `Datalogy.Gis.Core` - Core types, interfaces, and coordinate systems
  - `Datalogy.Gis.Domain` - Domain entities with fluent builder API
  - `Datalogy.Gis.Application` - Application services with MediatR and FluentValidation
- **Data Providers**:
  - `Datalogy.Gis.Data.Sqlite` - SQLite/Spatialite support with spatial queries
  - `Datalogy.Gis.Data.Postgres` - PostgreSQL/PostGIS integration
  - `Datalogy.Gis.Data.CosmosDb` - Azure Cosmos DB cloud-native storage
- **Rendering**:
  - `Datalogy.Gis.Rendering.Core` - Rendering abstractions
  - `Datalogy.Gis.Rendering.SkiaSharp` - SkiaSharp-based high-performance rendering
- **UI Components**:
  - `Datalogy.Gis.Maui` - .NET MAUI cross-platform map controls
  - `Datalogy.Gis.Blazor` - Blazor WebAssembly interactive map components
- **Testing**:
  - 43+ comprehensive unit and integration tests
  - Test coverage for core functionality, domain models, and data repositories
  - xUnit, FluentAssertions, and Moq testing stack
- **Sample Applications**:
  - MAUI Map Sample - Cross-platform mobile/desktop demo
  - Blazor Map Sample - WebAssembly interactive map demo
  - Quick Start guide with 5-minute setup
- **CI/CD**:
  - GitHub Actions workflows for automated build and test
  - Code quality checks with dotnet format
  - Multi-job pipeline with parallel execution
  - Automated test result collection
- **Documentation**:
  - Comprehensive README with quick start guide
  - Brand guidelines (BRAND_GUIDE.md)
  - Launch checklist (LAUNCH_CHECKLIST.md)
  - Quick start guide (QUICK_START.md)
  - Pull request template

### Changed
- Updated all package metadata to DatalogySoftware branding
- Migrated to .NET 8.0 with C# 12
- Modernized project structure with clean architecture
- Enhanced Feature entity with fluent builder pattern
- Improved repository pattern with async/await throughout

### Fixed
- MAUI workload installation issues on Linux CI runners
- Empty project compilation errors with placeholder classes
- Missing NetTopologySuite package reference in Domain project
- CI/CD pipeline compatibility with Linux build agents

### Technical Details
- **Target Framework**: .NET 8.0
- **Language Version**: C# 12
- **Key Dependencies**:
  - NetTopologySuite 2.5.0 - Geometry processing
  - SkiaSharp 2.88.6 - Graphics rendering
  - Microsoft.Data.Sqlite 8.0.0 - SQLite support
  - Npgsql 8.0.0 - PostgreSQL support
  - Microsoft.Azure.Cosmos 3.38.0 - Cosmos DB support
  - FluentValidation 11.9.0 - Validation
  - MediatR 12.2.0 - CQRS/Messaging

### Package Versions
All packages released as version `3.0.0-preview`:
- Datalogy.Gis.Core
- Datalogy.Gis.Domain
- Datalogy.Gis.Application
- Datalogy.Gis.Data.Sqlite
- Datalogy.Gis.Data.Postgres
- Datalogy.Gis.Data.CosmosDb
- Datalogy.Gis.Rendering.Core
- Datalogy.Gis.Rendering.SkiaSharp
- Datalogy.Gis.Maui
- Datalogy.Gis.Blazor

### Migration Notes
This is a major version release with breaking changes from previous versions:
- Namespace changes: All namespaces now use `Datalogy.Gis.*` pattern
- Package renaming: All packages now use `Datalogy.Gis.*` naming
- Minimum requirement: .NET 8.0 or higher

### Known Issues
- MAUI projects require platform-specific workloads for compilation
- Linux CI builds exclude MAUI projects due to workload limitations

---

## [Unreleased]

### Planned for 3.1.0
- Advanced styling engine with customizable map themes
- Vector tile support (MVT format)
- WMS/WFS/WCS service integration
- Real-time data streaming capabilities
- Enhanced spatial analytics

### Planned for 3.2.0
- 3D visualization support
- Machine learning integration for spatial predictions
- Augmented reality map overlays
- Advanced spatial analytics and clustering

---

## Earlier Versions

Earlier versions were released under different branding and are not compatible with 3.0.0+.
Please refer to the migration guide for upgrading from 2.x versions.

---

[3.0.0-preview]: https://github.com/datalogysoft/gis-framework/releases/tag/v3.0.0-preview
[Unreleased]: https://github.com/datalogysoft/gis-framework/compare/v3.0.0-preview...HEAD
