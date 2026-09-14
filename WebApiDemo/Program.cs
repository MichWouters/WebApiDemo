using Scalar.AspNetCore;
using WebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registreer UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBestellingService, BestellingService>();

// Registreer de Database Connectie
string? connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

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