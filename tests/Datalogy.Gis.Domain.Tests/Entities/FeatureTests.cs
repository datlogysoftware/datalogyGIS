using Datalogy.Gis.Domain.Entities;
using FluentAssertions;
using NetTopologySuite.Geometries;
using Xunit;

namespace Datalogy.Gis.Domain.Tests.Entities;

/// <summary>
/// Unit tests for Feature entity and FeatureBuilder
/// Part of DatalogySoftware GIS Framework
/// </summary>
public class FeatureTests
{
    [Fact]
    public void Feature_DefaultConstructor_ShouldCreateWithDefaults()
    {
        // Act
        var feature = new Feature();

        // Assert
        feature.Id.Should().NotBeNullOrEmpty();
        feature.Attributes.Should().NotBeNull().And.BeEmpty();
        feature.SRID.Should().Be(4326);
        feature.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        feature.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void FeatureBuilder_WithPoint_ShouldCreatePointFeature()
    {
        // Act
        var feature = Feature.Builder()
            .WithPoint(longitude: 28.9784, latitude: 41.0082)
            .Build();

        // Assert
        feature.Geometry.Should().NotBeNull();
        feature.Geometry.Should().BeOfType<Point>();
        var point = (Point)feature.Geometry!;
        point.X.Should().Be(28.9784);
        point.Y.Should().Be(41.0082);
        point.SRID.Should().Be(4326);
    }

    [Fact]
    public void FeatureBuilder_WithId_ShouldSetCustomId()
    {
        // Arrange
        var customId = "test-feature-123";

        // Act
        var feature = Feature.Builder()
            .WithId(customId)
            .Build();

        // Assert
        feature.Id.Should().Be(customId);
    }

    [Fact]
    public void FeatureBuilder_WithAttribute_ShouldAddAttribute()
    {
        // Act
        var feature = Feature.Builder()
            .WithAttribute("name", "Istanbul")
            .WithAttribute("population", 15_460_000)
            .Build();

        // Assert
        feature.Attributes.Should().HaveCount(2);
        feature.Attributes["name"].Should().Be("Istanbul");
        feature.Attributes["population"].Should().Be(15_460_000);
    }

    [Fact]
    public void FeatureBuilder_WithAttributes_ShouldAddMultipleAttributes()
    {
        // Arrange
        var attributes = new Dictionary<string, object?>
        {
            ["name"] = "Istanbul",
            ["country"] = "Turkey",
            ["population"] = 15_460_000
        };

        // Act
        var feature = Feature.Builder()
            .WithAttributes(attributes)
            .Build();

        // Assert
        feature.Attributes.Should().HaveCount(3);
        feature.Attributes.Should().Contain(attributes);
    }

    [Fact]
    public void FeatureBuilder_WithSRID_ShouldSetCustomSRID()
    {
        // Act
        var feature = Feature.Builder()
            .WithSRID(32635) // UTM Zone 35N
            .WithPoint(664274, 4550917)
            .Build();

        // Assert
        feature.SRID.Should().Be(32635);
        feature.Geometry!.SRID.Should().Be(32635);
    }

    [Fact]
    public void FeatureBuilder_WithGeometry_ShouldSetGeometry()
    {
        // Arrange
        var point = new Point(28.9784, 41.0082) { SRID = 4326 };

        // Act
        var feature = Feature.Builder()
            .WithGeometry(point)
            .Build();

        // Assert
        feature.Geometry.Should().BeSameAs(point);
    }

    [Fact]
    public void FeatureBuilder_FluentChaining_ShouldBuildCompleteFeature()
    {
        // Act
        var feature = Feature.Builder()
            .WithId("istanbul-001")
            .WithPoint(28.9784, 41.0082)
            .WithAttribute("name", "Istanbul")
            .WithAttribute("country", "Turkey")
            .WithAttribute("population", 15_460_000)
            .WithAttribute("area_km2", 5461.0)
            .WithSRID(4326)
            .Build();

        // Assert
        feature.Id.Should().Be("istanbul-001");
        feature.Geometry.Should().NotBeNull();
        feature.Attributes.Should().HaveCount(4);
        feature.SRID.Should().Be(4326);
    }

    [Fact]
    public void Feature_Attributes_ShouldSupportNullValues()
    {
        // Act
        var feature = Feature.Builder()
            .WithAttribute("name", "Test")
            .WithAttribute("description", null)
            .Build();

        // Assert
        feature.Attributes.Should().ContainKey("description");
        feature.Attributes["description"].Should().BeNull();
    }

    [Fact]
    public void Feature_Attributes_ShouldSupportVariousTypes()
    {
        // Act
        var feature = Feature.Builder()
            .WithAttribute("string", "text")
            .WithAttribute("int", 42)
            .WithAttribute("double", 3.14)
            .WithAttribute("bool", true)
            .WithAttribute("date", DateTime.Parse("2024-01-01"))
            .Build();

        // Assert
        feature.Attributes["string"].Should().BeOfType<string>();
        feature.Attributes["int"].Should().BeOfType<int>();
        feature.Attributes["double"].Should().BeOfType<double>();
        feature.Attributes["bool"].Should().BeOfType<bool>();
        feature.Attributes["date"].Should().BeOfType<DateTime>();
    }

    [Fact]
    public void Feature_UpdatedAt_ShouldBeNullByDefault()
    {
        // Act
        var feature = new Feature();

        // Assert
        feature.UpdatedAt.Should().BeNull();
    }

    [Fact]
    public void Feature_UpdatedAt_ShouldBeSettable()
    {
        // Arrange
        var feature = new Feature();
        var updateTime = DateTime.UtcNow;

        // Act
        feature.UpdatedAt = updateTime;

        // Assert
        feature.UpdatedAt.Should().Be(updateTime);
    }

    [Fact]
    public void FeatureBuilder_WithSRID_BeforeGeometry_ShouldApplySRIDToGeometry()
    {
        // Act
        var feature = Feature.Builder()
            .WithSRID(3857) // Web Mercator
            .WithPoint(3213896, 5013926)
            .Build();

        // Assert
        feature.SRID.Should().Be(3857);
        feature.Geometry!.SRID.Should().Be(3857);
    }

    [Fact]
    public void FeatureBuilder_MultipleBuilds_ShouldCreateSameFeature()
    {
        // Arrange
        var builder = Feature.Builder()
            .WithId("test-001")
            .WithPoint(0, 0);

        // Act
        var feature1 = builder.Build();
        var feature2 = builder.Build();

        // Assert
        // Note: This test verifies that the builder returns the same reference
        // In a production scenario, you might want to create new instances
        feature1.Should().BeSameAs(feature2);
    }
}
