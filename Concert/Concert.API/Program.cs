using Concert.API.Data;
using DotEnv.Core;
using Microsoft.EntityFrameworkCore;
using Concert.Utility;
using Concert.API.Repositories;
using Concert.API.Mappings;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

// Environment variables management.
string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Read environment variables.
new EnvLoader().Load();
var envVarReader = new EnvReader();

// Get connectionStrings.
string connectionString = string.Empty;
string connectionStringAuth = string.Empty;
if (envName == SD.ENVIRONMENT_DEVELOPMENT)
{
    connectionString = envVarReader["DataBase_ConnectionString_Development"];
    connectionStringAuth = envVarReader["DataBase_ConnectionStringAuth_Development"];
}
else if (envName == SD.ENVIRONMENT_PRODUCTION)
{
    connectionString = Environment.GetEnvironmentVariable("DataBase_ConnectionString_Production");
    connectionStringAuth = Environment.GetEnvironmentVariable("DataBase_ConnectionStringAuth_Production");
}

// Get JWT parameters
string jwtSecretKey = envVarReader["Jwt_SecretKey"];
string jwtIssuer = envVarReader["Jwt_Issuer"];
string jwtAudience = envVarReader["Jwt_Audience"];


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
builder.Services.AddDbContext<ConcertAuthDbContext>(options =>
    options.UseSqlServer(connectionStringAuth));

// Add the repositories.
builder.Services.AddScoped<IArtistRepository, SqlArtistRepository>();
builder.Services.AddScoped<ISongRepository, SqlSongRepository>();

// Add Automapper.
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add Identity.
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Concert")
    .AddEntityFrameworkStores<ConcertAuthDbContext>()
    .AddDefaultTokenProviders(); // Used to generate tokens to reset passwords, change emails, etc.
// Configure password settings.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Add JWT Authentication.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
