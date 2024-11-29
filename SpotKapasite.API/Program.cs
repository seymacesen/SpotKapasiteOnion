using Microsoft.EntityFrameworkCore;
using SpotKapasite.Application.Interfaces;
using SpotKapasite.Application.Services;
using SpotKapasite.Domain.Interfaces;
using SpotKapasite.Infrastructure;
using SpotKapasite.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KapasiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IKapasiteRepository, KapasiteRepository>();
builder.Services.AddScoped<IKapasiteService, KapasiteService>();
builder.Services.AddScoped<IKapasiteSeederService, KapasiteSeederService>();


// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seederService = services.GetRequiredService<IKapasiteSeederService>();
    await seederService.SeedDataAsync(); // Seed data from JSON to the database
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
