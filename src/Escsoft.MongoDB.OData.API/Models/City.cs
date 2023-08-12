
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Escsoft.MongoDB.OData.API.Models
{
    public class City
    {
        public long Id { get; set; }
        public string CountryId { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public int Population { get; set; }

        public double Density { get; set; }

        public string[] Tags { get; set; }

        public double AverageTemperatureMin { get; set; }

        public double AverageTemperatureMax { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; }
    }
}