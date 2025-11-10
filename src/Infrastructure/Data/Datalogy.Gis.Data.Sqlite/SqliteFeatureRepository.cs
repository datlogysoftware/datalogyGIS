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
        _tableName = ValidateTableName(tableName ?? throw new ArgumentNullException(nameof(tableName)));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Validates and sanitizes table name to prevent SQL injection.
    /// </summary>
    private static string ValidateTableName(string tableName)
    {
        // Only allow alphanumeric characters and underscores
        if (!System.Text.RegularExpressions.Regex.IsMatch(tableName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
        {
            throw new ArgumentException(
                "Table name must start with a letter or underscore and contain only alphanumeric characters and underscores.",
                nameof(tableName));
        }
        return tableName;
    }

    public async Task<Feature> InsertAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        if (feature == null) throw new ArgumentNullException(nameof(feature));

        _logger.LogInformation("Inserting feature {FeatureId} into {TableName}", feature.Id, _tableName);

        try
        {
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

            _logger.LogInformation("Feature {FeatureId} inserted successfully", feature.Id);
            return feature;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting feature {FeatureId}", feature.Id);
            throw;
        }
    }

    public async Task<Feature> UpdateAsync(Feature feature, CancellationToken cancellationToken = default)
    {
        if (feature == null) throw new ArgumentNullException(nameof(feature));

        _logger.LogInformation("Updating feature {FeatureId} in {TableName}", feature.Id, _tableName);

        try
        {
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

            var rowsAffected = await cmd.ExecuteNonQueryAsync(cancellationToken);

            if (rowsAffected == 0)
            {
                _logger.LogWarning("Feature {FeatureId} not found for update", feature.Id);
            }
            else
            {
                _logger.LogInformation("Feature {FeatureId} updated successfully", feature.Id);
            }

            return feature;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating feature {FeatureId}", feature.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));

        _logger.LogInformation("Deleting feature {FeatureId} from {TableName}", id, _tableName);

        try
        {
            var sql = $"DELETE FROM {_tableName} WHERE id = @id";

            using var cmd = new SqliteCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@id", id.ToString());

            var result = await cmd.ExecuteNonQueryAsync(cancellationToken);
            var deleted = result > 0;

            if (deleted)
            {
                _logger.LogInformation("Feature {FeatureId} deleted successfully", id);
            }
            else
            {
                _logger.LogWarning("Feature {FeatureId} not found for deletion", id);
            }

            return deleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting feature {FeatureId}", id);
            throw;
        }
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
        try
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
                try
                {
                    feature.Attributes = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object?>>(attributesJson)
                        ?? new Dictionary<string, object?>();
                }
                catch (System.Text.Json.JsonException ex)
                {
                    _logger.LogWarning(ex, "Failed to deserialize attributes for feature {FeatureId}, using empty dictionary", feature.Id);
                    feature.Attributes = new Dictionary<string, object?>();
                }
            }

            var updatedAtOrdinal = reader.GetOrdinal("updated_at");
            if (!reader.IsDBNull(updatedAtOrdinal))
            {
                feature.UpdatedAt = reader.GetDateTime(updatedAtOrdinal);
            }

            return feature;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading feature from database");
            throw;
        }
    }
}
