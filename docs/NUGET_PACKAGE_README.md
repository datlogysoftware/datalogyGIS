# DatalogySoftware GIS Framework

**Modern GIS framework for .NET - Build powerful geospatial applications with ease**

## Installation

```bash
dotnet add package Datalogy.Gis.Core
```

## Quick Start

```csharp
using Datalogy.Gis.Domain.Entities;

// Create a feature
var feature = Feature.Builder()
    .WithPoint(longitude: -122.4194, latitude: 37.7749)
    .WithAttribute("name", "San Francisco")
    .WithAttribute("population", 873_965)
    .Build();

// Save to database
await repository.InsertAsync(feature);

// Query by location
var nearby = await repository.QueryBBoxAsync(
    minLon: -123, minLat: 37,
    maxLon: -122, maxLat: 38
);
```

## Features

- Modern .NET 8 architecture
- Multiple database providers (SQLite, PostgreSQL, Cosmos DB)
- High-performance rendering with SkiaSharp
- Cross-platform (Windows, Linux, macOS, iOS, Android)
- MAUI and Blazor UI components
- Comprehensive spatial operations

## Documentation

Visit [docs.datalogysoft.com/gis](https://docs.datalogysoft.com/gis) for full documentation.

## Support

- Email: support@datalogysoft.com
- GitHub: [github.com/datalogysoft/gis-framework](https://github.com/datalogysoft/gis-framework)

## License

MIT License - Copyright © 2024 DatalogySoftware
