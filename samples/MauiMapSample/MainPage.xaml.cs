using Datalogy.Gis.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatalogySoftware.Gis.Samples.MauiMap;

/// <summary>
/// Main page for DatalogySoftware GIS MAUI Sample
/// Demonstrates MapView control usage
/// </summary>
public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private double _centerLatitude = 41.0082;
    private double _centerLongitude = 28.9784;
    private int _zoomLevel = 10;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public double CenterLatitude
    {
        get => _centerLatitude;
        set
        {
            _centerLatitude = value;
            OnPropertyChanged();
        }
    }

    public double CenterLongitude
    {
        get => _centerLongitude;
        set
        {
            _centerLongitude = value;
            OnPropertyChanged();
        }
    }

    public int ZoomLevel
    {
        get => _zoomLevel;
        set
        {
            _zoomLevel = Math.Clamp(value, 1, 20);
            OnPropertyChanged();
        }
    }

    private void OnMapTapped(object? sender, MapTappedEventArgs e)
    {
        CoordinatesLabel.Text = $"Tapped: {e.Latitude:F6}, {e.Longitude:F6}";
    }

    private void OnIstanbulClicked(object? sender, EventArgs e)
    {
        CenterLatitude = 41.0082;
        CenterLongitude = 28.9784;
        ZoomLevel = 11;
        CoordinatesLabel.Text = "Istanbul, Turkey";
    }

    private void OnLondonClicked(object? sender, EventArgs e)
    {
        CenterLatitude = 51.5074;
        CenterLongitude = -0.1276;
        ZoomLevel = 11;
        CoordinatesLabel.Text = "London, United Kingdom";
    }

    private void OnNewYorkClicked(object? sender, EventArgs e)
    {
        CenterLatitude = 40.7128;
        CenterLongitude = -74.0060;
        ZoomLevel = 11;
        CoordinatesLabel.Text = "New York, United States";
    }

    private void OnZoomInClicked(object? sender, EventArgs e)
    {
        ZoomLevel++;
    }

    private void OnZoomOutClicked(object? sender, EventArgs e)
    {
        ZoomLevel--;
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
