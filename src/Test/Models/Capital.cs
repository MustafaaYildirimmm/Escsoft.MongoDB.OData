

namespace Escsoft.MongoDB.OData.API.Models
{
    public class Capital
    {
        public long CityId { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public int Population { get; set; }
    }
}