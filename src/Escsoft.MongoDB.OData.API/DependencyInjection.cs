
using Escsoft.MongoDB.OData.API;
using Escsoft.MongoDB.OData.API.Models;
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
    public static IServiceCollection AddMongoDbOdata(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoDb").GetValue<string>("Uri");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Cannot read mongoDb connection settings");
        }

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<City>("Cities");
        modelBuilder.EntitySet<Region>("Regions");
        modelBuilder.EntitySet<CountryModel>("Countries");

        services.AddControllers().AddOData(
            options =>
            {
                options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents(
                    "odata",
                    modelBuilder.GetEdmModel());
            });

        return services;
    }

    public static IApplicationBuilder UseMongoDbOdata(this IApplicationBuilder app,
        IHostApplicationLifetime applicationLifetime)
    {
        applicationLifetime.ApplicationStarted.Register(() => {
            OnStarted(app.ApplicationServices);
        });
        
        return app;
    }

    private static void OnStarted(IServiceProvider serviceProvider)
    {
        DatabaseInitialize.Initialize(serviceProvider);
    }
}
