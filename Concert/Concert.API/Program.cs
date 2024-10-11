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
using Microsoft.Extensions.FileProviders;
using Serilog;
using Concert.API.Middlewares;

// Create builder.
var builder = WebApplication.CreateBuilder(args);

// Environment variables management.

string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Read environment variables.
new EnvLoader().Load();
var envVarReader = new EnvReader();

// Get connectionStrings and JWT parameters.
string connectionString = string.Empty;
string connectionStringAuth = string.Empty;
string jwtSecretKey = string.Empty;
string jwtIssuer = string.Empty;
string jwtAudience = string.Empty;

if (envName == Environments.Development)
{
    connectionString = envVarReader["DataBase_ConnectionString"];
    connectionStringAuth = envVarReader["DataBaseAuth_ConnectionString"];
    jwtSecretKey = envVarReader["Jwt_SecretKey"];
    jwtIssuer = envVarReader["Jwt_Issuer"];
    jwtAudience = envVarReader["Jwt_Audience"];
}
else if (envName == Environments.Production)
{
    connectionString = Environment.GetEnvironmentVariable("DataBase_ConnectionString");
    connectionStringAuth = Environment.GetEnvironmentVariable("DataBaseAuth_ConnectionString");
    jwtSecretKey = Environment.GetEnvironmentVariable("Jwt_SecretKey");
    jwtIssuer = Environment.GetEnvironmentVariable("Jwt_Issuer");
    jwtAudience = Environment.GetEnvironmentVariable("Jwt_Audience");
}

// Add services to the container.

// Add Serilog
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(SD.LOGS_FILE_FULL_PATH, rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

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
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Concert API",
        Version = "v1"
    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
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
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

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
        ValidAudiences = new[] { jwtAudience },
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Connection from the request to the physical path.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), SD.IMAGES_FOLDER_NAME)),
    RequestPath = $"/{SD.IMAGES_FOLDER_NAME}"
});

app.MapControllers();

app.Run();