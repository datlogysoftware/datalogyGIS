namespace Datalogy.Gis.Rendering.Core.Interfaces;

/// <summary>
/// Interface for map rendering engines.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
public interface IMapRenderer
{
    /// <summary>
    /// Renders a map tile as an image.
    /// </summary>
    /// <param name="width">Width of the output image in pixels</param>
    /// <param name="height">Height of the output image in pixels</param>
    /// <param name="bbox">Bounding box of the area to render</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Rendered image as byte array</returns>
    Task<byte[]> RenderTileAsync(
        int width,
        int height,
        BoundingBox bbox,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Renders a map tile and saves it to a file.
    /// </summary>
    Task RenderTileToFileAsync(
        string filePath,
        int width,
        int height,
        BoundingBox bbox,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents a geographic bounding box
/// </summary>
public record BoundingBox(double MinLon, double MinLat, double MaxLon, double MaxLat)
{
    public double Width => MaxLon - MinLon;
    public double Height => MaxLat - MinLat;
    public (double Lon, double Lat) Center => ((MinLon + MaxLon) / 2, (MinLat + MaxLat) / 2);
}
