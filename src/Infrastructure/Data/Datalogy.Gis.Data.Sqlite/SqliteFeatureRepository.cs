using Datalogy.Gis.Core.Interfaces;
using Datalogy.Gis.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Datalogy.Gis.Data.Sqlite;

/// <summary>
/// SQLite implementation of feature repository with Spatialite support.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
public class SqliteFeatureRepository : IFeatureRepository<Feature>
{
    private readonly SqliteConnection _connection;
    private readonly string _tableName;
    private readonly ILogger<SqliteFeatureRepository> _logger;

    public SqliteFeatureRepository(
        SqliteConnection connection,
        string tableName,
        ILogger<SqliteFeatureRepository> logger)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Feature> InsertAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Inserting feature {FeatureId} into {TableName}", feature.Id, _tableName);

        var wkbWriter = new WKBWriter();
        var geometryWkb = feature.Geometry != null ? wkbWriter.Write(feature.Geometry) : null;

        var sql = $@"
            INSERT INTO {_tableName} (id, geometry, attributes, srid, created_at)
            VALUES (@id, @geometry, @attributes, @srid, @created_at)";

        using var cmd = new SqliteCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@id", feature.Id);
        cmd.Parameters.AddWithValue("@geometry", geometryWkb ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@attributes", System.Text.Json.JsonSerializer.Serialize(feature.Attributes));
        cmd.Parameters.AddWithValue("@srid", feature.SRID);
        cmd.Parameters.AddWithValue("@created_at", feature.CreatedAt);

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        return feature;
    }

    public async Task<Feature> UpdateAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating feature {FeatureId} in {TableName}", feature.Id, _tableName);

        feature.UpdatedAt = DateTime.UtcNow;

        var wkbWriter = new WKBWriter();
        var geometryWkb = feature.Geometry != null ? wkbWriter.Write(feature.Geometry) : null;

        var sql = $@"
            UPDATE {_tableName}
            SET geometry = @geometry, attributes = @attributes, srid = @srid, updated_at = @updated_at
            WHERE id = @id";

        using var cmd = new SqliteCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@id", feature.Id);
        cmd.Parameters.AddWithValue("@geometry", geometryWkb ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@attributes", System.Text.Json.JsonSerializer.Serialize(feature.Attributes));
        cmd.Parameters.AddWithValue("@srid", feature.SRID);
        cmd.Parameters.AddWithValue("@updated_at", feature.UpdatedAt);

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        return feature;
    }

    public async Task<bool> DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting feature {FeatureId} from {TableName}", id, _tableName);

        var sql = $"DELETE FROM {_tableName} WHERE id = @id";

        using var cmd = new SqliteCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@id", id.ToString());

        var result = await cmd.ExecuteNonQueryAsync(cancellationToken);
        return result > 0;
    }

    public async Task<Feature?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        var sql = $"SELECT * FROM {_tableName} WHERE id = @id";

        using var cmd = new SqliteCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@id", id.ToString());

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken))
        {
            return ReadFeature(reader);
        }

        return null;
    }

    public async Task<IEnumerable<Feature>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sql = $"SELECT * FROM {_tableName}";
        var features = new List<Feature>();

        using var cmd = new SqliteCommand(sql, _connection);
        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            features.Add(ReadFeature(reader));
        }

        return features;
    }

    public async Task<IEnumerable<Feature>> QueryBBoxAsync(
        double minLon, double minLat, double maxLon, double maxLat,
        CancellationToken cancellationToken = default)
    {
        var sql = $@"
            SELECT * FROM {_tableName}
            WHERE MbrIntersects(geometry, BuildMbr(@minLon, @minLat, @maxLon, @maxLat))";

        var features = new List<Feature>();

        using var cmd = new SqliteCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@minLon", minLon);
        cmd.Parameters.AddWithValue("@minLat", minLat);
        cmd.Parameters.AddWithValue("@maxLon", maxLon);
        cmd.Parameters.AddWithValue("@maxLat", maxLat);

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            features.Add(ReadFeature(reader));
        }

        return features;
    }

    private Feature ReadFeature(SqliteDataReader reader)
    {
        var feature = new Feature
        {
            Id = reader.GetString(reader.GetOrdinal("id")),
            SRID = reader.GetInt32(reader.GetOrdinal("srid")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"))
        };

        var geometryOrdinal = reader.GetOrdinal("geometry");
        if (!reader.IsDBNull(geometryOrdinal))
        {
            var wkbReader = new WKBReader();
            var geometryBytes = (byte[])reader.GetValue(geometryOrdinal);
            feature.Geometry = wkbReader.Read(geometryBytes);
        }

        var attributesOrdinal = reader.GetOrdinal("attributes");
        if (!reader.IsDBNull(attributesOrdinal))
        {
            var attributesJson = reader.GetString(attributesOrdinal);
            feature.Attributes = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object?>>(attributesJson)
                ?? new Dictionary<string, object?>();
        }

        var updatedAtOrdinal = reader.GetOrdinal("updated_at");
        if (!reader.IsDBNull(updatedAtOrdinal))
        {
            feature.UpdatedAt = reader.GetDateTime(updatedAtOrdinal);
        }

        return feature;
    }
}
