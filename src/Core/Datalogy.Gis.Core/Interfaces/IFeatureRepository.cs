namespace Datalogy.Gis.Core.Interfaces;

/// <summary>
/// Repository interface for feature storage and retrieval.
/// Part of DatalogySoftware GIS Framework.
/// </summary>
/// <typeparam name="TFeature">Type of feature entity</typeparam>
public interface IFeatureRepository<TFeature> where TFeature : class
{
    /// <summary>
    /// Inserts a new feature into the repository.
    /// </summary>
    Task<TFeature> InsertAsync(TFeature feature, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing feature.
    /// </summary>
    Task<TFeature> UpdateAsync(TFeature feature, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a feature by its identifier.
    /// </summary>
    Task<bool> DeleteAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a feature by its identifier.
    /// </summary>
    Task<TFeature?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all features in the repository.
    /// </summary>
    Task<IEnumerable<TFeature>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries features within a bounding box.
    /// </summary>
    Task<IEnumerable<TFeature>> QueryBBoxAsync(
        double minLon, double minLat, double maxLon, double maxLat,
        CancellationToken cancellationToken = default);
}
