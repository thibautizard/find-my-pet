using FindMyPet.Api;
using FindMyPet.Api.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheAnimalsAPI.Animals;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IRepository<Animal>, AnimalRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
