using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Datalogy.Gis.Maui.Controls;

/// <summary>
/// MAUI MapView control for displaying interactive maps.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
public class MapView : View
{
    public static readonly BindableProperty CenterLatitudeProperty =
        BindableProperty.Create(
            nameof(CenterLatitude),
            typeof(double),
            typeof(MapView),
            0.0);

    public static readonly BindableProperty CenterLongitudeProperty =
        BindableProperty.Create(
            nameof(CenterLongitude),
            typeof(double),
            typeof(MapView),
            0.0);

    public static readonly BindableProperty ZoomLevelProperty =
        BindableProperty.Create(
            nameof(ZoomLevel),
            typeof(int),
            typeof(MapView),
            10);

    /// <summary>
    /// Center latitude of the map view
    /// </summary>
    public double CenterLatitude
    {
        get => (double)GetValue(CenterLatitudeProperty);
        set => SetValue(CenterLatitudeProperty, value);
    }

    /// <summary>
    /// Center longitude of the map view
    /// </summary>
    public double CenterLongitude
    {
        get => (double)GetValue(CenterLongitudeProperty);
        set => SetValue(CenterLongitudeProperty, value);
    }

    /// <summary>
    /// Zoom level (1-20)
    /// </summary>
    public int ZoomLevel
    {
        get => (int)GetValue(ZoomLevelProperty);
        set => SetValue(ZoomLevelProperty, value);
    }

    /// <summary>
    /// Event raised when the map is tapped
    /// </summary>
    public event EventHandler<MapTappedEventArgs>? MapTapped;

    protected virtual void OnMapTapped(MapTappedEventArgs e)
    {
        MapTapped?.Invoke(this, e);
    }
}

/// <summary>
/// Event arguments for map tap events
/// </summary>
public class MapTappedEventArgs : EventArgs
{
    public double Latitude { get; }
    public double Longitude { get; }
    public Point Position { get; }

    public MapTappedEventArgs(double latitude, double longitude, Point position)
    {
        Latitude = latitude;
        Longitude = longitude;
        Position = position;
    }
}
