using Datalogy.Gis.Core.Coordinates;
using NetTopologySuite.Geometries;

namespace Datalogy.Gis.Domain.Entities;

/// <summary>
/// Represents a geographic feature with geometry and attributes.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
public class Feature
{
    /// <summary>
    /// Unique identifier for the feature
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Geometry of the feature (Point, LineString, Polygon, etc.)
    /// </summary>
    public Geometry? Geometry { get; set; }

    /// <summary>
    /// Attributes (properties) of the feature
    /// </summary>
    public Dictionary<string, object?> Attributes { get; set; } = new();

    /// <summary>
    /// Coordinate Reference System Identifier (e.g., 4326 for WGS84)
    /// </summary>
    public int SRID { get; set; } = 4326;

    /// <summary>
    /// Timestamp when the feature was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the feature was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Builder for creating features with a fluent API
    /// </summary>
    public static FeatureBuilder Builder() => new();
}

/// <summary>
/// Fluent builder for creating Feature instances
/// </summary>
public class FeatureBuilder
{
    private readonly Feature _feature = new();

    public FeatureBuilder WithId(string id)
    {
        _feature.Id = id;
        return this;
    }

    public FeatureBuilder WithGeometry(Geometry geometry)
    {
        _feature.Geometry = geometry;
        return this;
    }

    public FeatureBuilder WithPoint(double longitude, double latitude)
    {
        _feature.Geometry = new Point(longitude, latitude) { SRID = _feature.SRID };
        return this;
    }

    public FeatureBuilder WithAttribute(string key, object? value)
    {
        _feature.Attributes[key] = value;
        return this;
    }

    public FeatureBuilder WithAttributes(Dictionary<string, object?> attributes)
    {
        foreach (var (key, value) in attributes)
        {
            _feature.Attributes[key] = value;
        }
        return this;
    }

    public FeatureBuilder WithSRID(int srid)
    {
        _feature.SRID = srid;
        if (_feature.Geometry != null)
        {
            _feature.Geometry.SRID = srid;
        }
        return this;
    }

    public Feature Build() => _feature;
}
