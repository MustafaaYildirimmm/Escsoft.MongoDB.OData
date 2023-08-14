using Escsoft.MongoDB.OData.API.Models;
using System.Collections.Generic;

namespace Escsoft.MongoDB.OData.API.Models
{
    public class CountryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Capital Capital { get; set; }
        public Region[] Regions { get; set; }
        public int Population { get; set; }
        public int Area { get; set; }
        public IEnumerable<City> Cities { get; set; }
    }
}