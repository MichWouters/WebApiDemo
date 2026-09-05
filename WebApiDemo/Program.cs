using System.Reflection;
using Mapster;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Vertel Mapster om het huidige project te scannen op IRegister klassen (zoals onze MapperProfile)
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registreer UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registreer de Database Connectie
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registreer de DbContext met de PostgreSQL provider (Npgsql)
builder.Services.AddDbContext<WebAPIDemoContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // <-- Voeg dit toe voor de interactieve UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseHttpsRedirection();

app.Run();