using Microsoft.AspNetCore.Mvc;
using TheAnimalsAPI.Animals;

var builder = WebApplication.CreateBuilder(args);
var animals = new List<Animal>
{
  new() { Id = 1, Name = "Simba" },
  new() { Id = 2, Name = "Nala" },
};

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

var animalRoute = app.MapGroup("animals");

animalRoute.MapGet(
  string.Empty,
  () =>
  {
    return Results.Ok(animals.Select(animal => new GetAnimalResponse { Name = animal.Name }));
  }
);

animalRoute.MapGet(
  "{id:int}",
  ([FromRoute] int id) =>
  {
    var animal = animals.SingleOrDefault(e => e.Id == id);
    if (animal == null)
    {
      return Results.NotFound();
    }
    return Results.Ok(new GetAnimalResponse { Name = animal.Name });
  }
);

animalRoute.MapPost(
  string.Empty,
  ([FromBody] CreateAnimalRequest animal, HttpContext context) =>
  {
    var newAnimal = new Animal { Id = animals.Max(e => e.Id) + 1, Name = animal.Name };
    animals.Add(newAnimal);
    return Results.Created($"/animals/${newAnimal.Id}", animal);
  }
);

animalRoute.MapPut(
  "{id:int}",
  ([FromBody] Animal animal, int id) =>
  {
    var existingAnimal = animals.SingleOrDefault(e => e.Id == id);
    if (existingAnimal == null)
    {
      return Results.NotFound();
    }

    existingAnimal.Name = animal.Name;

    return Results.Ok(existingAnimal);
  }
);

app.Run();

public partial class Program { }
