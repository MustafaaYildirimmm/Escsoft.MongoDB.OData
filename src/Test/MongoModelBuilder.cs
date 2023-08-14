using Escsoft.MongoDB.OData.API.Models;
using Microsoft.OData.ModelBuilder;

namespace Test
{
    public static class MongoModelBuilder
    {
        public static ODataConventionModelBuilder GetModelBuilder()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<City>("Cities");

            return modelBuilder;
        }
    }
}
