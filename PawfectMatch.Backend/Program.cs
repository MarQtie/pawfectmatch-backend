using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.Services;
using PawfectMatch.Backend.Services.Interfaces;
using PawfectMatch.Backend.Services.Implementations;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load .env File
Env.Load();

// Supabase Config
var connectionString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("Supabase");

var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL")
                  ?? builder.Configuration["Supabase:Url"];

var supabaseApiKey = Environment.GetEnvironmentVariable("SUPABASE_API_KEY")
                       ?? builder.Configuration["Supabase:ApiKey"];

// JWT Secret
var key = Environment.GetEnvironmentVariable("JWT_SECRET");
if (string.IsNullOrEmpty(key))
    throw new Exception("JWT_SECRET not set in .env file");

var tokenKey = Encoding.ASCII.GetBytes(key);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add User-Defined Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAdoptionRequestService, AdoptionRequestService>();

// Add Swagger/OpenAPI with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PawfectMatch API",
        Version = "v1",
        Description = "REST API for managing users, pets, adoption requests, and logs."
    });

    // Add JWT Authorization to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(tokenKey)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawfectMatch API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

// **Important:** Add authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
