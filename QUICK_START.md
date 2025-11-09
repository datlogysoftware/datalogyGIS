# DatalogySoftware GIS Framework - Quick Start Guide

## 🚀 5 Dakikada Başlayın

### 1. Installation

```bash
# Core paketleri yükleyin
dotnet add package Datalogy.Gis.Core
dotnet add package Datalogy.Gis.Domain

# Data provider seçin (birini veya hepsini)
dotnet add package Datalogy.Gis.Data.Sqlite
dotnet add package Datalogy.Gis.Data.Postgres
dotnet add package Datalogy.Gis.Data.CosmosDb
```

### 2. İlk Feature'ınızı Oluşturun

```csharp
using Datalogy.Gis.Domain.Entities;

// Basit bir nokta feature'ı oluştur
var istanbul = Feature.Builder()
    .WithPoint(longitude: 28.9784, latitude: 41.0082)
    .WithAttribute("name", "Istanbul")
    .WithAttribute("population", 15_460_000)
    .WithAttribute("country", "Turkey")
    .Build();

Console.WriteLine($"Created: {istanbul.Attributes["name"]}");
Console.WriteLine($"Location: {istanbul.Geometry}");
```

### 3. Veritabanına Kaydedin

```csharp
using Datalogy.Gis.Data.Sqlite;
using Microsoft.Data.Sqlite;

// SQLite bağlantısı oluştur
var connection = new SqliteConnection("Data Source=cities.db");
await connection.OpenAsync();

// Repository oluştur
var repository = new SqliteFeatureRepository(
    connection,
    tableName: "cities",
    logger);

// Feature'ı kaydet
await repository.InsertAsync(istanbul);

// Geri al
var retrieved = await repository.GetByIdAsync(istanbul.Id);
Console.WriteLine($"Retrieved: {retrieved.Attributes["name"]}");
```

### 4. Spatial Query

```csharp
// Bounding box içindeki tüm feature'ları al
var features = await repository.QueryBBoxAsync(
    minLon: 28.0,
    minLat: 40.0,
    maxLon: 30.0,
    maxLat: 42.0
);

foreach (var feature in features)
{
    Console.WriteLine($"Found: {feature.Attributes["name"]}");
}
```

---

## 📱 Platform Specific Kullanım

### MAUI Application

**1. Package yükle:**
```bash
dotnet add package Datalogy.Gis.Maui
```

**2. XAML'de kullan:**
```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:gis="clr-namespace:Datalogy.Gis.Maui.Controls;assembly=Datalogy.Gis.Maui">

    <gis:MapView
        x:Name="MapView"
        CenterLatitude="41.0082"
        CenterLongitude="28.9784"
        ZoomLevel="10"
        MapTapped="OnMapTapped" />

</ContentPage>
```

**3. Code-behind:**
```csharp
private void OnMapTapped(object sender, MapTappedEventArgs e)
{
    Console.WriteLine($"Tapped: {e.Latitude}, {e.Longitude}");
}
```

### Blazor Application

**1. Package yükle:**
```bash
dotnet add package Datalogy.Gis.Blazor
```

**2. Razor component:**
```razor
@page "/map"
@using Datalogy.Gis.Blazor.Components

<MapComponent
    CenterLatitude="41.0082"
    CenterLongitude="28.9784"
    ZoomLevel="10"
    Width="100%"
    Height="600px"
    ShowZoomControls="true"
    OnMapClick="HandleMapClick" />

@code {
    private void HandleMapClick((double Lat, double Lon) coords)
    {
        Console.WriteLine($"Clicked: {coords.Lat}, {coords.Lon}");
    }
}
```

---

## 🎯 Yaygın Kullanım Senaryoları

### Senaryo 1: Şehir Veritabanı

```csharp
// Birden fazla şehir ekle
var cities = new[]
{
    Feature.Builder()
        .WithPoint(-0.1276, 51.5074)
        .WithAttribute("name", "London")
        .WithAttribute("population", 9_002_488)
        .Build(),

    Feature.Builder()
        .WithPoint(2.3522, 48.8566)
        .WithAttribute("name", "Paris")
        .WithAttribute("population", 2_165_423)
        .Build(),

    Feature.Builder()
        .WithPoint(139.6917, 35.6895)
        .WithAttribute("name", "Tokyo")
        .WithAttribute("population", 13_960_000)
        .Build()
};

foreach (var city in cities)
{
    await repository.InsertAsync(city);
}
```

### Senaryo 2: Custom SRID (UTM)

```csharp
// UTM Zone 35N koordinat sistemi kullan
var surveyPoint = Feature.Builder()
    .WithSRID(32635) // EPSG:32635
    .WithPoint(x: 664274, y: 4550917)
    .WithAttribute("type", "Survey Marker")
    .WithAttribute("name", "Point Alpha")
    .Build();

await repository.InsertAsync(surveyPoint);
```

### Senaryo 3: Attribute Güncelleme

```csharp
// Feature'ı al
var feature = await repository.GetByIdAsync("feature-id");

// Attribute'ları güncelle
feature.Attributes["population"] = 16_000_000;
feature.Attributes["last_updated"] = DateTime.UtcNow;

// Kaydet
await repository.UpdateAsync(feature);
```

---

## 🔧 İleri Düzey Özellikler

### Custom Validation

```csharp
using FluentValidation;

public class FeatureValidator : AbstractValidator<Feature>
{
    public FeatureValidator()
    {
        RuleFor(f => f.Geometry)
            .NotNull()
            .WithMessage("Geometry is required");

        RuleFor(f => f.Attributes)
            .Must(a => a.ContainsKey("name"))
            .WithMessage("Name attribute is required");
    }
}
```

### Dependency Injection

```csharp
// Startup.cs / Program.cs
services.AddScoped<IFeatureRepository<Feature>>(sp =>
{
    var connection = new SqliteConnection("Data Source=app.db");
    connection.Open();
    return new SqliteFeatureRepository(
        connection,
        "features",
        sp.GetRequiredService<ILogger<SqliteFeatureRepository>>());
});
```

### Async/Await Best Practices

```csharp
// ✅ İyi
var features = await repository.GetAllAsync(cancellationToken);

// ❌ Kötü
var features = repository.GetAllAsync().Result;

// ✅ Batch operations
var tasks = cities.Select(c => repository.InsertAsync(c));
await Task.WhenAll(tasks);
```

---

## 📚 Örnekler

Framework ile birlikte gelen örneklere bakın:

1. **QuickStart** - `samples/QuickStart/`
   - Temel feature oluşturma
   - Console uygulaması

2. **MAUI Sample** - `samples/MauiMapSample/`
   - Cross-platform mobile/desktop
   - Interactive map
   - City navigation

3. **Blazor Sample** - `samples/BlazorMapSample/`
   - WebAssembly application
   - Interactive components
   - Modern UI

---

## 🆘 Yardım & Destek

### Dokümantasyon
- **Full Docs**: https://docs.datalogysoft.com/gis
- **API Reference**: https://docs.datalogysoft.com/gis/api
- **GitHub**: https://github.com/datalogysoft/gis-framework

### Topluluk
- **GitHub Issues**: Sorun bildirin
- **GitHub Discussions**: Soru sorun
- **Email**: support@datalogysoft.com

### Yaygın Sorunlar

**Q: "NetTopologySuite bulunamadı" hatası**
```bash
dotnet add package NetTopologySuite
```

**Q: "SQLite database oluşturulamadı"**
```bash
# Spatialite extension yükleyin
# Linux: apt-get install libsqlite3-mod-spatialite
# macOS: brew install spatialite-tools
# Windows: NuGet package otomatik yükler
```

**Q: "SRID hataları"**
```csharp
// Geometry ve Feature SRID'leri eşleştirin
feature.Geometry.SRID = feature.SRID;
```

---

## 🎓 Sonraki Adımlar

1. **Tutorial Serisi**: Advanced spatial queries
2. **Video Eğitimler**: YouTube channel
3. **Blog Posts**: Medium/Dev.to articles
4. **Community Samples**: GitHub discussions

---

**Başarılar!** 🚀

DatalogySoftware GIS Framework ile harika şeyler inşa edin!
