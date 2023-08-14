using Microsoft.OData.ModelBuilder;

namespace Escsoft.MongoDB.OData
{
    public class MongoDBODataOptions
    {
        public ODataConventionModelBuilder ModelBuilder { get; set; }
        public string MongoUri { get; set; }
        public int MaxLenght { get; set; } = 100000;

    }
}
