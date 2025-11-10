using Datalogy.Gis.Domain.Entities;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Datalogy.Gis.Data.Sqlite.Tests;

/// <summary>
/// Integration tests for SqliteFeatureRepository
/// Part of DatalogySoftware GIS Framework
/// </summary>
public class SqliteFeatureRepositoryTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly SqliteFeatureRepository _repository;
    private readonly Mock<ILogger<SqliteFeatureRepository>> _loggerMock;

    public SqliteFeatureRepositoryTests()
    {
        // Setup in-memory SQLite database
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        _loggerMock = new Mock<ILogger<SqliteFeatureRepository>>();
        _repository = new SqliteFeatureRepository(_connection, "features", _loggerMock.Object);

        // Create test table
        CreateTestTable();
    }

    private void CreateTestTable()
    {
        var createTableSql = @"
            CREATE TABLE features (
                id TEXT PRIMARY KEY,
                geometry BLOB,
                attributes TEXT,
                srid INTEGER,
                created_at TEXT,
                updated_at TEXT
            )";

        using var cmd = new SqliteCommand(createTableSql, _connection);
        cmd.ExecuteNonQuery();
    }

    [Fact]
    public async Task InsertAsync_ValidFeature_ShouldInsertSuccessfully()
    {
        // Arrange
        var feature = Feature.Builder()
            .WithId("test-001")
            .WithPoint(28.9784, 41.0082)
            .WithAttribute("name", "Istanbul")
            .Build();

        // Act
        var result = await _repository.InsertAsync(feature);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("test-001");
    }

    [Fact]
    public async Task GetByIdAsync_ExistingFeature_ShouldReturnFeature()
    {
        // Arrange
        var feature = Feature.Builder()
            .WithId("test-002")
            .WithPoint(28.9784, 41.0082)
            .WithAttribute("name", "Istanbul")
            .WithAttribute("population", 15_460_000)
            .Build();

        await _repository.InsertAsync(feature);

        // Act
        var result = await _repository.GetByIdAsync("test-002");

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be("test-002");
        result.Attributes["name"].ToString().Should().Be("Istanbul");
        result.Attributes["population"].ToString().Should().Be("15460000");
    }

    [Fact]
    public async Task GetByIdAsync_NonExistentFeature_ShouldReturnNull()
    {
        // Act
        var result = await _repository.GetByIdAsync("non-existent");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ExistingFeature_ShouldUpdateSuccessfully()
    {
        // Arrange
        var feature = Feature.Builder()
            .WithId("test-003")
            .WithPoint(28.9784, 41.0082)
            .WithAttribute("name", "Istanbul")
            .Build();

        await _repository.InsertAsync(feature);

        // Modify feature
        feature.Attributes["name"] = "Istanbul Updated";
        feature.Attributes["new_field"] = "new_value";

        // Act
        var result = await _repository.UpdateAsync(feature);

        // Assert
        result.Should().NotBeNull();
        result.UpdatedAt.Should().NotBeNull();

        var updated = await _repository.GetByIdAsync("test-003");
        updated!.Attributes["name"].ToString().Should().Be("Istanbul Updated");
        updated.Attributes["new_field"].ToString().Should().Be("new_value");
    }

    [Fact]
    public async Task DeleteAsync_ExistingFeature_ShouldDeleteSuccessfully()
    {
        // Arrange
        var feature = Feature.Builder()
            .WithId("test-004")
            .WithPoint(28.9784, 41.0082)
            .Build();

        await _repository.InsertAsync(feature);

        // Act
        var result = await _repository.DeleteAsync("test-004");

        // Assert
        result.Should().BeTrue();

        var deleted = await _repository.GetByIdAsync("test-004");
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_NonExistentFeature_ShouldReturnFalse()
    {
        // Act
        var result = await _repository.DeleteAsync("non-existent");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllAsync_MultipleFeatures_ShouldReturnAll()
    {
        // Arrange
        var features = new[]
        {
            Feature.Builder().WithId("city-001").WithPoint(28.9784, 41.0082).WithAttribute("name", "Istanbul").Build(),
            Feature.Builder().WithId("city-002").WithPoint(-0.1276, 51.5074).WithAttribute("name", "London").Build(),
            Feature.Builder().WithId("city-003").WithPoint(2.3522, 48.8566).WithAttribute("name", "Paris").Build()
        };

        foreach (var feature in features)
        {
            await _repository.InsertAsync(feature);
        }

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetAllAsync_EmptyRepository_ShouldReturnEmpty()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Constructor_NullConnection_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Action act = () => new SqliteFeatureRepository(null!, "test", _loggerMock.Object);
        act.Should().Throw<ArgumentNullException>().WithParameterName("connection");
    }

    [Fact]
    public async Task Constructor_NullTableName_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Action act = () => new SqliteFeatureRepository(_connection, null!, _loggerMock.Object);
        act.Should().Throw<ArgumentNullException>().WithParameterName("tableName");
    }

    [Fact]
    public async Task Constructor_NullLogger_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Action act = () => new SqliteFeatureRepository(_connection, "test", null!);
        act.Should().Throw<ArgumentNullException>().WithParameterName("logger");
    }

    [Theory]
    [InlineData("DROP TABLE")]
    [InlineData("features; DROP TABLE users--")]
    [InlineData("features' OR '1'='1")]
    [InlineData("123InvalidName")]
    [InlineData("table-name")]
    [InlineData("table.name")]
    public void Constructor_InvalidTableName_ShouldThrowArgumentException(string invalidTableName)
    {
        // Act & Assert
        Action act = () => new SqliteFeatureRepository(_connection, invalidTableName, _loggerMock.Object);
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Table name must start with a letter or underscore*");
    }

    [Fact]
    public async Task InsertAsync_NullFeature_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Func<Task> act = async () => await _repository.InsertAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("feature");
    }

    [Fact]
    public async Task UpdateAsync_NullFeature_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Func<Task> act = async () => await _repository.UpdateAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("feature");
    }

    [Fact]
    public async Task DeleteAsync_NullId_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Func<Task> act = async () => await _repository.DeleteAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("id");
    }

    [Fact]
    public async Task InsertAsync_FeatureWithComplexAttributes_ShouldPreserveTypes()
    {
        // Arrange
        var feature = Feature.Builder()
            .WithId("complex-001")
            .WithPoint(0, 0)
            .WithAttribute("string", "text")
            .WithAttribute("number", 42)
            .WithAttribute("decimal", 3.14)
            .WithAttribute("boolean", true)
            .WithAttribute("null", null)
            .Build();

        // Act
        await _repository.InsertAsync(feature);
        var result = await _repository.GetByIdAsync("complex-001");

        // Assert
        result.Should().NotBeNull();
        result!.Attributes["string"].Should().Be("text");
        result.Attributes.ContainsKey("number").Should().BeTrue();
        result.Attributes.ContainsKey("boolean").Should().BeTrue();
        result.Attributes.ContainsKey("null").Should().BeTrue();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
