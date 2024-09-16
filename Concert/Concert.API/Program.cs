using Concert.API.Data;
using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using Concert.Utility;
using Concert.API.Repositories;
using Concert.API.Mappings;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

// Environment variables management.
string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Read environment variables.
new EnvLoader().Load();
var envVarReader = new EnvReader();

// Get connectionString.
string connectionString = string.Empty;
if (envName == SD.ENVIRONMENT_DEVELOPMENT)
{
    connectionString = envVarReader["DataBase_ConnectionString_Development"];
}
else if (envName == SD.ENVIRONMENT_PRODUCTION)
{
    connectionString = Environment.GetEnvironmentVariable("DataBase_ConnectionString_Production");
}

// Create builder.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Workaround for API call error: "The JSON value could not be converted to System.TimeSpan"
    options.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

// Add the database service.
builder.Services.AddDbContext<ConcertDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add the repositories.
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();

// Add Automapper.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
