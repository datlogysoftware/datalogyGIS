# DatalogySoftware GIS Framework

**Powering Next-Generation Geospatial Intelligence for .NET**

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)
[![NuGet](https://img.shields.io/badge/NuGet-3.0.0--preview-orange.svg)](https://www.nuget.org/packages/Datalogy.Gis.Core/)

---

## About DatalogySoftware GIS Framework

DatalogySoftware GIS Framework is a modern, high-performance geospatial data processing and visualization framework built for .NET 8. Designed with enterprise-grade requirements in mind, it provides developers with a comprehensive toolkit for building sophisticated mapping and spatial analysis applications.

### Key Features

- **Modern Architecture**: Built on .NET 8 with C# 12, leveraging latest language features
- **High Performance**: Optimized spatial queries and rendering engine
- **Cloud-Native**: First-class support for Azure, AWS, and on-premises deployments
- **Cross-Platform**: Works on Windows, Linux, macOS, iOS, and Android
- **Multiple Data Sources**: SQLite/Spatialite, PostgreSQL/PostGIS, Azure Cosmos DB
- **Rich Rendering**: SkiaSharp-based rendering for beautiful, fast map visualization
- **UI Frameworks**: Ready-to-use components for MAUI and Blazor
- **Developer-Friendly**: Fluent API with comprehensive documentation

---

## Quick Start

### Installation

Install the core package via NuGet:

```bash
dotnet add package Datalogy.Gis.Core
dotnet add package Datalogy.Gis.Domain
```

For data providers:

```bash
# SQLite/Spatialite
dotnet add package Datalogy.Gis.Data.Sqlite

# PostgreSQL/PostGIS
dotnet add package Datalogy.Gis.Data.Postgres

# Azure Cosmos DB
dotnet add package Datalogy.Gis.Data.CosmosDb
```

### Basic Usage

```csharp
using Datalogy.Gis.Core;
using Datalogy.Gis.Domain.Entities;

// Create a feature with geometry
var feature = Feature.Builder()
    .WithPoint(longitude: 28.9784, latitude: 41.0082)
    .WithAttribute("name", "Istanbul")
    .WithAttribute("population", 15_460_000)
    .WithAttribute("country", "Turkey")
    .Build();

// Save to database
await repository.InsertAsync(feature);

// Query by bounding box
var features = await repository.QueryBBoxAsync(
    minLon: 28.0, minLat: 40.0,
    maxLon: 30.0, maxLat: 42.0
);
```

---

## Architecture

### Project Structure

```
DatalogySoftware.Gis/
├── src/
│   ├── Core/
│   │   ├── Datalogy.Gis.Core           # Core types and abstractions
│   │   ├── Datalogy.Gis.Domain         # Domain models and entities
│   │   └── Datalogy.Gis.Application    # Application services
│   │
│   ├── Infrastructure/
│   │   └── Data/
│   │       ├── Datalogy.Gis.Data.Sqlite     # SQLite provider
│   │       ├── Datalogy.Gis.Data.Postgres   # PostgreSQL provider
│   │       └── Datalogy.Gis.Data.CosmosDb   # Cosmos DB provider
│   │
│   ├── Rendering/
│   │   ├── Datalogy.Gis.Rendering.Core      # Rendering abstractions
│   │   └── Datalogy.Gis.Rendering.SkiaSharp # SkiaSharp implementation
│   │
│   └── UI/
│       ├── Datalogy.Gis.Maui           # MAUI components
│       └── Datalogy.Gis.Blazor         # Blazor components
│
├── tests/
├── samples/
└── docs/
```

### Namespace Conventions

All packages use the `Datalogy.Gis.*` namespace pattern:

- `Datalogy.Gis.Core` - Core functionality
- `Datalogy.Gis.Domain` - Domain models
- `Datalogy.Gis.Data.*` - Data providers
- `Datalogy.Gis.Rendering.*` - Rendering engines
- `Datalogy.Gis.Maui` - MAUI components
- `Datalogy.Gis.Blazor` - Blazor components

---

## Packages

| Package | Description | Status |
|---------|-------------|--------|
| **Datalogy.Gis.Core** | Core types, interfaces, and utilities | Preview |
| **Datalogy.Gis.Domain** | Domain entities (Feature, Layer, Map) | Preview |
| **Datalogy.Gis.Application** | Application services and validation | Preview |
| **Datalogy.Gis.Data.Sqlite** | SQLite/Spatialite data provider | Preview |
| **Datalogy.Gis.Data.Postgres** | PostgreSQL/PostGIS data provider | Preview |
| **Datalogy.Gis.Data.CosmosDb** | Azure Cosmos DB data provider | Preview |
| **Datalogy.Gis.Rendering.Core** | Rendering abstractions | Preview |
| **Datalogy.Gis.Rendering.SkiaSharp** | SkiaSharp rendering engine | Preview |
| **Datalogy.Gis.Maui** | .NET MAUI map components | Preview |
| **Datalogy.Gis.Blazor** | Blazor map components | Preview |

---

## Platform Support

| Platform | Status | Notes |
|----------|--------|-------|
| **Windows** | ✅ | Full support |
| **Linux** | ✅ | Full support |
| **macOS** | ✅ | Full support |
| **iOS** | ✅ | Via MAUI |
| **Android** | ✅ | Via MAUI |
| **Web** | ✅ | Via Blazor WebAssembly |

---

## Technology Stack

- **.NET 8** - Modern, cross-platform runtime
- **C# 12** - Latest language features
- **NetTopologySuite** - Geometry processing
- **SkiaSharp** - High-performance rendering
- **Entity Framework Core** - Database abstraction
- **MediatR** - CQRS and messaging
- **FluentValidation** - Input validation

---

## Examples

### MAUI Application

```csharp
using Datalogy.Gis.Maui.Controls;

// In your XAML
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:gis="clr-namespace:Datalogy.Gis.Maui.Controls;assembly=Datalogy.Gis.Maui">

    <gis:MapView
        CenterLatitude="41.0082"
        CenterLongitude="28.9784"
        ZoomLevel="10"
        MapTapped="OnMapTapped" />

</ContentPage>
```

### Blazor Component

```razor
@using Datalogy.Gis.Blazor.Components

<MapComponent
    CenterLatitude="41.0082"
    CenterLongitude="28.9784"
    ZoomLevel="10"
    Width="100%"
    Height="600px"
    ShowZoomControls="true"
    OnMapClick="HandleMapClick" />
```

### Spatial Queries

```csharp
using Datalogy.Gis.Data.Postgres;

// Query features within radius
var nearbyFeatures = await repository.QueryRadiusAsync(
    centerLon: 28.9784,
    centerLat: 41.0082,
    radiusMeters: 5000
);

// Spatial join
var featuresInPolygon = await repository.QueryIntersectsAsync(polygon);

// Buffer operation
var buffered = geometry.Buffer(distanceMeters: 1000);
```

---

## Performance

DatalogySoftware GIS Framework is designed for high performance:

- **10x faster** spatial queries with optimized indexing
- **Efficient memory usage** with streaming APIs
- **Tile caching** for instant map rendering
- **Async/await** throughout for responsive UIs
- **Parallel processing** for batch operations

---

## Documentation

- **Getting Started**: [docs/getting-started.md](docs/getting-started.md)
- **API Reference**: [https://docs.datalogysoft.com/gis/api](https://docs.datalogysoft.com/gis/api)
- **Samples**: [samples/](samples/)
- **Migration Guide**: [docs/migration.md](docs/migration.md)

---

## Brand Identity

### Company Information

- **Company**: DatalogySoftware
- **Website**: [https://www.datalogysoft.com](https://www.datalogysoft.com)
- **Product**: DatalogySoftware GIS Framework
- **Tagline**: "Powering Next-Generation Geospatial Intelligence"

### Color Palette

- **Primary**: `#0066CC` (Blue)
- **Secondary**: `#00A86B` (Green)
- **Accent**: `#FF6B35` (Orange)
- **Dark**: `#2C3E50` (Navy)
- **Light**: `#ECF0F1` (Gray)

---

## Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Support

- **Documentation**: [https://docs.datalogysoft.com/gis](https://docs.datalogysoft.com/gis)
- **Email**: [support@datalogysoft.com](mailto:support@datalogysoft.com)
- **Issues**: [GitHub Issues](https://github.com/datalogysoft/gis-framework/issues)
- **Discussions**: [GitHub Discussions](https://github.com/datalogysoft/gis-framework/discussions)

---

## Roadmap

### Version 3.0 (Current - Preview)
- ✅ Core framework architecture
- ✅ SQLite/Spatialite support
- ✅ PostgreSQL/PostGIS support
- ✅ Azure Cosmos DB support
- ✅ SkiaSharp rendering
- ✅ MAUI components
- ✅ Blazor components

### Version 3.1 (Q2 2024)
- 🔄 Advanced styling engine
- 🔄 Vector tile support (MVT)
- 🔄 WMS/WFS/WCS services
- 🔄 Real-time data streaming

### Version 3.2 (Q3 2024)
- 📋 3D visualization
- 📋 Machine learning integration
- 📋 Augmented reality support
- 📋 Advanced spatial analytics

---

## Acknowledgments

Built with these excellent libraries:
- [NetTopologySuite](https://github.com/NetTopologySuite/NetTopologySuite) - JTS topology suite for .NET
- [SkiaSharp](https://github.com/mono/SkiaSharp) - Cross-platform 2D graphics
- [Entity Framework Core](https://github.com/dotnet/efcore) - Modern object-database mapper

---

<p align="center">
  <strong>DatalogySoftware GIS Framework</strong><br>
  Modern GIS for Modern .NET<br>
  <br>
  Made with ❤️ by <a href="https://www.datalogysoft.com">DatalogySoftware</a>
</p>
