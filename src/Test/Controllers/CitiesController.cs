using Escsoft.MongoDB.OData.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Escsoft.MongoDB.OData.API.Controllers
{
    public class CitiesController : ODataController
    {
        private readonly IQueryable<City> _cities;
        public CitiesController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("escsoft-mongo-odata-test");
            _cities = database.GetCollection<City>("cities").AsQueryable();
        }

        [MongoEnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(_cities);
        }
    }
}