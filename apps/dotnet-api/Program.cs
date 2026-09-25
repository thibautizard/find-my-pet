using Microsoft.AspNetCore.Mvc;

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

var animalRoute = app.MapGroup("animals");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

animalRoute.MapGet(
  string.Empty,
  () =>
  {
    return Results.Ok(animals);
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
    return Results.Ok(animal);
  }
);

animalRoute.MapPost(
  string.Empty,
  ([FromBody] Animal animal, HttpContext context) =>
  {
    animal.Id = animals.Max(e => e.Id) + 1;
    animals.Add(animal);
    return Results.Created($"/animals/${animal.Id}", animal);
  }
);

app.Run();
