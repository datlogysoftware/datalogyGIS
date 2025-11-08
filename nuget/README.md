# DatalogySoftware GIS Framework

**Modern GIS framework for .NET - Build powerful geospatial applications with ease**

## 📦 Installation

```bash
dotnet add package Datalogy.Gis.Core
```

## 🚀 Quick Start

```csharp
using Datalogy.Gis.Domain.Entities;

// Create a feature with geometry
var feature = Feature.Builder()
    .WithPoint(longitude: -122.4194, latitude: 37.7749)
    .WithAttribute("name", "San Francisco")
    .WithAttribute("population", 873_965)
    .Build();

// Save to database
await repository.InsertAsync(feature);

// Query by bounding box
var features = await repository.QueryBBoxAsync(
    minLon: -123, minLat: 37,
    maxLon: -122, maxLat: 38
);
```

## ✨ Key Features

- **Modern Architecture** - Built on .NET 8 with C# 12
- **High Performance** - Optimized spatial queries and rendering
- **Cross-Platform** - Windows, Linux, macOS, iOS, Android, Web
- **Multiple Data Sources** - SQLite, PostgreSQL, Cosmos DB
- **Rich Rendering** - SkiaSharp-based visualization
- **UI Components** - MAUI and Blazor ready-to-use controls
- **Developer-Friendly** - Fluent API with comprehensive documentation

## 📚 Available Packages

| Package | Description | NuGet |
|---------|-------------|-------|
| **Datalogy.Gis.Core** | Core types and abstractions | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Core.svg)](https://nuget.org/packages/Datalogy.Gis.Core) |
| **Datalogy.Gis.Domain** | Domain models (Feature, Layer, Map) | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Domain.svg)](https://nuget.org/packages/Datalogy.Gis.Domain) |
| **Datalogy.Gis.Data.Sqlite** | SQLite/Spatialite provider | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Data.Sqlite.svg)](https://nuget.org/packages/Datalogy.Gis.Data.Sqlite) |
| **Datalogy.Gis.Data.Postgres** | PostgreSQL/PostGIS provider | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Data.Postgres.svg)](https://nuget.org/packages/Datalogy.Gis.Data.Postgres) |
| **Datalogy.Gis.Maui** | .NET MAUI map components | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Maui.svg)](https://nuget.org/packages/Datalogy.Gis.Maui) |
| **Datalogy.Gis.Blazor** | Blazor map components | [![NuGet](https://img.shields.io/nuget/v/Datalogy.Gis.Blazor.svg)](https://nuget.org/packages/Datalogy.Gis.Blazor) |

## 🎯 Use Cases

- **Enterprise GIS Applications** - Mission-critical spatial data management
- **Mobile Mapping Apps** - Cross-platform iOS/Android applications
- **Web Mapping Solutions** - Interactive Blazor web applications
- **Location-Based Services** - Real-time geospatial analytics
- **IoT & Telematics** - Asset tracking and fleet management
- **Smart Cities** - Urban planning and management systems

## 📖 Documentation

- **Full Documentation**: [docs.datalogysoft.com/gis](https://docs.datalogysoft.com/gis)
- **API Reference**: [docs.datalogysoft.com/gis/api](https://docs.datalogysoft.com/gis/api)
- **Samples**: [github.com/datalogysoft/gis-framework/samples](https://github.com/datalogysoft/gis-framework/tree/main/samples)
- **Getting Started Guide**: [docs.datalogysoft.com/gis/getting-started](https://docs.datalogysoft.com/gis/getting-started)

## 💡 Examples

### Creating Features

```csharp
// Simple point feature
var point = Feature.Builder()
    .WithPoint(28.9784, 41.0082)
    .WithAttribute("city", "Istanbul")
    .Build();

// Feature with custom SRID
var utmPoint = Feature.Builder()
    .WithSRID(32635) // UTM Zone 35N
    .WithPoint(664274, 4550917)
    .WithAttribute("survey_point", "Alpha")
    .Build();
```

### Spatial Queries

```csharp
// Query features in bounding box
var features = await repository.QueryBBoxAsync(
    minLon: 28.0, minLat: 40.0,
    maxLon: 30.0, maxLat: 42.0
);

// Get all features
var all = await repository.GetAllAsync();

// Get by ID
var feature = await repository.GetByIdAsync("feature-001");
```

### MAUI Integration

```xml
<ContentPage xmlns:gis="clr-namespace:Datalogy.Gis.Maui.Controls">
    <gis:MapView
        CenterLatitude="41.0082"
        CenterLongitude="28.9784"
        ZoomLevel="10" />
</ContentPage>
```

### Blazor Integration

```razor
@using Datalogy.Gis.Blazor.Components

<MapComponent
    CenterLatitude="41.0082"
    CenterLongitude="28.9784"
    ZoomLevel="10"
    Width="100%"
    Height="600px" />
```

## 🛠️ Technology Stack

- **.NET 8** - Modern, cross-platform runtime
- **C# 12** - Latest language features
- **NetTopologySuite** - Geometry processing
- **SkiaSharp** - High-performance rendering
- **Entity Framework Core** - Database abstraction

## 🤝 Support

- **Email**: support@datalogysoft.com
- **GitHub Issues**: [github.com/datalogysoft/gis-framework/issues](https://github.com/datalogysoft/gis-framework/issues)
- **Discussions**: [github.com/datalogysoft/gis-framework/discussions](https://github.com/datalogysoft/gis-framework/discussions)

## 📄 License

MIT License - Copyright © 2024 DatalogySoftware

See [LICENSE](https://github.com/datalogysoft/gis-framework/blob/main/LICENSE) for details.

---

<p align="center">
  <strong>DatalogySoftware GIS Framework</strong><br>
  Powering Next-Generation Geospatial Intelligence<br>
  <br>
  Made with ❤️ by <a href="https://www.datalogysoft.com">DatalogySoftware</a>
</p>
