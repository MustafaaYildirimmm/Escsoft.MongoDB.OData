
using Microsoft.OData.Edm;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Escsoft.MongoDB.OData;

public static class ProjectionExtensions
{
    public static ProjectionDefinition<TSource> Combine<TSource>(this List<ProjectionDefinition<TSource>> definitions)
        => definitions.Count == 1 ? definitions[0] : Builders<TSource>.Projection.Combine(definitions);

    public static ProjectionDefinition<TSource> Project<TSource>(
        this IEnumerable<IEdmStructuralProperty> properties, string path = "")
    {
        if (!properties.Any())
            return new BsonDocumentProjectionDefinition<TSource>(new BsonDocument());

        var result = new List<ProjectionDefinition<TSource>>();
        foreach (var prop in properties)
        {
            var name = string.IsNullOrEmpty(path) ? prop.Name : $"{path}.{prop.Name}";
            result.Add(Builders<TSource>.Projection.Include(name));
        }

        return result.Combine();
    }
}
