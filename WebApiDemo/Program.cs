using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApiDemo.Data;
using WebAPIDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registreer UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registreer de Database Connectie
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

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

app.Run();