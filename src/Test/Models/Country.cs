
using Escsoft.MongoDB.OData.API.Models;

namespace Escsoft.MongoDB.OData.API.Models
{
    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Capital Capital { get; set; }
        public Region[] Regions { get; set; }
        public int Population { get; set; }
        public int Area { get; set; }
    }
}