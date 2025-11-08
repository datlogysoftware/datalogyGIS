namespace Datalogy.Gis.Core.Coordinates;

/// <summary>
/// Represents a geographic coordinate with latitude and longitude.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
public readonly record struct Coordinate
{
    /// <summary>
    /// Latitude in decimal degrees (-90 to 90)
    /// </summary>
    public double Latitude { get; init; }

    /// <summary>
    /// Longitude in decimal degrees (-180 to 180)
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    /// Optional elevation in meters
    /// </summary>
    public double? Elevation { get; init; }

    public Coordinate(double latitude, double longitude, double? elevation = null)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90");

        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180");

        Latitude = latitude;
        Longitude = longitude;
        Elevation = elevation;
    }

    public override string ToString() =>
        Elevation.HasValue
            ? $"({Latitude:F6}, {Longitude:F6}, {Elevation:F2}m)"
            : $"({Latitude:F6}, {Longitude:F6})";
}
