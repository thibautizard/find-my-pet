using FindMyPet.Api;
using FindMyPet.Api.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TheAnimalsAPI.Animals;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IRepository<Animal>, AnimalRepository>();

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
  ([FromServices] IRepository<Animal> repository) =>
  {
    var animals = repository.GetAll();
    return Results.Ok(animals.Select(animal => new GetAnimalResponse { Name = animal.Name }));
  }
);

animalRoute.MapGet(
  "{id:int}",
  ([FromRoute] int id, [FromServices] IRepository<Animal> repository) =>
  {
    var animal = repository.GetById(id);
    if (animal == null)
    {
      return Results.NotFound();
    }
    return Results.Ok(new GetAnimalResponse { Name = animal.Name });
  }
);

animalRoute.MapPost(
  string.Empty,
  (
    [FromBody] CreateAnimalRequest animalRequest,
    HttpContext context,
    [FromServices] IRepository<Animal> repository
  ) =>
  {
    var newAnimal = new Animal { Name = animalRequest.Name };
    repository.Create(newAnimal);
    return Results.Created($"/animals/${newAnimal.Id}", animalRequest);
  }
);

animalRoute.MapPut(
  "{id:int}",
  ([FromBody] Animal animalRequest, int id, [FromServices] IRepository<Animal> repository) =>
  {
    var animals = repository.GetAll();
    var existingAnimal = animals.SingleOrDefault(e => e.Id == id);
    if (existingAnimal == null)
    {
      return Results.NotFound();
    }

    existingAnimal.Name = animalRequest.Name;

    repository.Update(existingAnimal);
    return Results.Ok(existingAnimal);
  }
);

app.Run();

public partial class Program { }
