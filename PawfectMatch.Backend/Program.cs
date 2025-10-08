using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using PawfectMatch.Backend.Data;
using PawfectMatch.Backend.Services;
using PawfectMatch.Backend.Services.Interfaces;
using PawfectMatch.Backend.Services.Implementations;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("Supabase");

var supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL")
                  ?? builder.Configuration["Supabase:Url"];

var supabaseApiKey = Environment.GetEnvironmentVariable("SUPABASE_API_KEY")
                       ?? builder.Configuration["Supabase:ApiKey"];

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

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PawfectMatch API",
        Version = "v1",
        Description = "REST API for managing users, pets, adoption requests, and logs."
    });
});

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawfectMatch API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
