
using Escsoft.MongoDB.OData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.ModelBuilder;
using MongoDB.Driver;
using System;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDbOdata(this IServiceCollection services, Action<MongoDBODataOptions> odataOptions)
    {
        var options = new MongoDBODataOptions();
        
        odataOptions(options);

        var connectionString = options.MongoUri;
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Cannot read mongoDb connection settings");
        }

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        var modelBuilder = new ODataConventionModelBuilder();
        //modelBuilder.EntitySet<City>("Cities");
        //modelBuilder.EntitySet<Region>("Regions");
        //modelBuilder.EntitySet<CountryModel>("Countries");

        services
            .AddControllers()
            .AddOData(
            odataOptions =>
            {
                odataOptions.Select()
                .Filter()
                .OrderBy()
                .Expand()
                .Count()
                .SetMaxTop(options.MaxLenght)
                .AddRouteComponents(
                    "odata",
                    options.ModelBuilder.GetEdmModel());
            });

        return services;
    }

}
