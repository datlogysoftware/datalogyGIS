using Datalogy.Gis.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DatalogySoftware.Gis.Samples.QuickStart;

/// <summary>
/// Quick start sample for DatalogySoftware GIS Framework
/// Demonstrates basic feature creation and manipulation
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("DatalogySoftware GIS Framework - Quick Start Sample");
        Console.WriteLine("====================================================");
        Console.WriteLine();

        // Example 1: Create a simple point feature
        Console.WriteLine("Example 1: Creating a point feature");
        var city = Feature.Builder()
            .WithPoint(longitude: 28.9784, latitude: 41.0082)
            .WithAttribute("name", "Istanbul")
            .WithAttribute("population", 15_460_000)
            .WithAttribute("country", "Turkey")
            .WithAttribute("continent", "Europe/Asia")
            .Build();

        Console.WriteLine($"Created feature: {city.Id}");
        Console.WriteLine($"  Location: {city.Geometry}");
        Console.WriteLine($"  Attributes: {string.Join(", ", city.Attributes.Select(kv => $"{kv.Key}={kv.Value}"))}");
        Console.WriteLine();

        // Example 2: Create multiple features
        Console.WriteLine("Example 2: Creating multiple city features");
        var cities = new[]
        {
            Feature.Builder()
                .WithPoint(-0.1276, 51.5074)
                .WithAttribute("name", "London")
                .WithAttribute("population", 9_002_488)
                .WithAttribute("country", "United Kingdom")
                .Build(),

            Feature.Builder()
                .WithPoint(2.3522, 48.8566)
                .WithAttribute("name", "Paris")
                .WithAttribute("population", 2_165_423)
                .WithAttribute("country", "France")
                .Build(),

            Feature.Builder()
                .WithPoint(139.6917, 35.6895)
                .WithAttribute("name", "Tokyo")
                .WithAttribute("population", 13_960_000)
                .WithAttribute("country", "Japan")
                .Build()
        };

        foreach (var c in cities)
        {
            Console.WriteLine($"  - {c.Attributes["name"]}: {c.Geometry}");
        }
        Console.WriteLine();

        // Example 3: Feature with custom SRID
        Console.WriteLine("Example 3: Feature with custom coordinate system (UTM Zone 35N)");
        var utmFeature = Feature.Builder()
            .WithId("utm-point-001")
            .WithPoint(x: 664274, y: 4550917) // UTM coordinates
            .WithSRID(32635) // EPSG:32635 - WGS 84 / UTM zone 35N
            .WithAttribute("name", "Survey Point Alpha")
            .WithAttribute("type", "Survey Marker")
            .Build();

        Console.WriteLine($"Created UTM feature: {utmFeature.Id}");
        Console.WriteLine($"  SRID: {utmFeature.SRID}");
        Console.WriteLine($"  Geometry: {utmFeature.Geometry}");
        Console.WriteLine();

        // Example 4: Accessing feature properties
        Console.WriteLine("Example 4: Working with feature attributes");
        var poi = Feature.Builder()
            .WithPoint(13.4050, 52.5200)
            .WithAttribute("name", "Brandenburg Gate")
            .WithAttribute("type", "Monument")
            .WithAttribute("city", "Berlin")
            .WithAttribute("built", 1791)
            .WithAttribute("height_m", 26.0)
            .Build();

        Console.WriteLine($"Point of Interest: {poi.Attributes["name"]}");
        Console.WriteLine($"  Type: {poi.Attributes["type"]}");
        Console.WriteLine($"  Location: {poi.Attributes["city"]}");
        Console.WriteLine($"  Built: {poi.Attributes["built"]}");
        Console.WriteLine($"  Height: {poi.Attributes["height_m"]}m");
        Console.WriteLine();

        Console.WriteLine("====================================================");
        Console.WriteLine("Sample completed successfully!");
        Console.WriteLine();
        Console.WriteLine("Next steps:");
        Console.WriteLine("  1. Try the database samples (Sqlite, PostgreSQL)");
        Console.WriteLine("  2. Explore rendering samples (SkiaSharp)");
        Console.WriteLine("  3. Build UI samples (MAUI, Blazor)");
        Console.WriteLine();
        Console.WriteLine("Visit https://docs.datalogysoft.com/gis for full documentation");

        await Task.CompletedTask;
    }
}
