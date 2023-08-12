using Escsoft.MongoDB.OData.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Escsoft.MongoDB.OData.API.Controllers
{
    public class CountriesController : ODataController
    {
        private readonly IQueryable<CountryModel> _countries;

        public CountriesController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(DatabaseInitialize.DatabaseName);

            var countries = database.GetCollection<Country>("countries").AsQueryable();
            var places = database.GetCollection<City>("cities").AsQueryable();

            _countries = countries.GroupJoin(
                places,
                c => c.Id,
                p => p.CountryId,
                (c, p) => new CountryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Capital = c.Capital,
                    Population = c.Population,
                    Area = c.Area,
                    Cities = p,
                    Regions = c.Regions
                });
        }

        [MongoEnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(_countries);
        }
    }
}