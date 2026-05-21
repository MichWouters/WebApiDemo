using Scalar.AspNetCore;
using WebAPIDemo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Link Dependency Interface aan implementerende klasse
builder.Services.AddScoped<ILaptopRepository, InMemoryRepository>();

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