using Datalogy.Gis.Core.Coordinates;
using FluentAssertions;
using Xunit;

namespace Datalogy.Gis.Core.Tests.Coordinates;

/// <summary>
/// Unit tests for Coordinate struct
/// Part of DatalogySoftware GIS Framework
/// </summary>
public class CoordinateTests
{
    [Fact]
    public void Constructor_ValidCoordinates_ShouldCreateCoordinate()
    {
        // Arrange & Act
        var coordinate = new Coordinate(latitude: 41.0082, longitude: 28.9784);

        // Assert
        coordinate.Latitude.Should().Be(41.0082);
        coordinate.Longitude.Should().Be(28.9784);
        coordinate.Elevation.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithElevation_ShouldCreateCoordinate()
    {
        // Arrange & Act
        var coordinate = new Coordinate(
            latitude: 41.0082,
            longitude: 28.9784,
            elevation: 100.5);

        // Assert
        coordinate.Latitude.Should().Be(41.0082);
        coordinate.Longitude.Should().Be(28.9784);
        coordinate.Elevation.Should().Be(100.5);
    }

    [Theory]
    [InlineData(-91, 0)]
    [InlineData(91, 0)]
    [InlineData(-90.1, 0)]
    [InlineData(90.1, 0)]
    public void Constructor_InvalidLatitude_ShouldThrowArgumentOutOfRangeException(
        double latitude, double longitude)
    {
        // Act & Assert
        Action act = () => new Coordinate(latitude, longitude);
        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("latitude");
    }

    [Theory]
    [InlineData(0, -181)]
    [InlineData(0, 181)]
    [InlineData(0, -180.1)]
    [InlineData(0, 180.1)]
    public void Constructor_InvalidLongitude_ShouldThrowArgumentOutOfRangeException(
        double latitude, double longitude)
    {
        // Act & Assert
        Action act = () => new Coordinate(latitude, longitude);
        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("longitude");
    }

    [Theory]
    [InlineData(-90, -180)]
    [InlineData(90, 180)]
    [InlineData(0, 0)]
    public void Constructor_BoundaryCoordinates_ShouldCreateCoordinate(
        double latitude, double longitude)
    {
        // Act
        var coordinate = new Coordinate(latitude, longitude);

        // Assert
        coordinate.Latitude.Should().Be(latitude);
        coordinate.Longitude.Should().Be(longitude);
    }

    [Fact]
    public void ToString_WithoutElevation_ShouldFormatCorrectly()
    {
        // Arrange
        var coordinate = new Coordinate(41.008200, 28.978400);

        // Act
        var result = coordinate.ToString();

        // Assert
        result.Should().Be("(41.008200, 28.978400)");
    }

    [Fact]
    public void ToString_WithElevation_ShouldFormatCorrectly()
    {
        // Arrange
        var coordinate = new Coordinate(41.008200, 28.978400, 100.5);

        // Act
        var result = coordinate.ToString();

        // Assert
        result.Should().Be("(41.008200, 28.978400, 100.50m)");
    }

    [Fact]
    public void Equality_SameCoordinates_ShouldBeEqual()
    {
        // Arrange
        var coord1 = new Coordinate(41.0082, 28.9784);
        var coord2 = new Coordinate(41.0082, 28.9784);

        // Act & Assert
        coord1.Should().Be(coord2);
        (coord1 == coord2).Should().BeTrue();
    }

    [Fact]
    public void Equality_DifferentCoordinates_ShouldNotBeEqual()
    {
        // Arrange
        var coord1 = new Coordinate(41.0082, 28.9784);
        var coord2 = new Coordinate(40.0082, 28.9784);

        // Act & Assert
        coord1.Should().NotBe(coord2);
        (coord1 != coord2).Should().BeTrue();
    }

    [Fact]
    public void Equality_WithAndWithoutElevation_ShouldNotBeEqual()
    {
        // Arrange
        var coord1 = new Coordinate(41.0082, 28.9784);
        var coord2 = new Coordinate(41.0082, 28.9784, 100);

        // Act & Assert
        coord1.Should().NotBe(coord2);
    }
}
