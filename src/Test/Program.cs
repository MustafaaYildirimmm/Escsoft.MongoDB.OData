using Microsoft.Extensions.Configuration;
using Microsoft.OData.ModelBuilder;
using MongoDB.Driver.Core.Configuration;
using Test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongoDbOdata(options =>
{
    options.MongoUri =  builder.Configuration.GetSection("MongoDb").GetValue<string>("Uri");
    options.ModelBuilder = MongoModelBuilder.GetModelBuilder();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
