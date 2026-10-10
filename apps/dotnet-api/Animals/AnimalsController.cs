using FindMyPet.Api.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TheAnimalsAPI.Animals;

public class AnimalsController : BaseController
{
  private readonly IRepository<Animal> _repository;

  public AnimalsController(IRepository<Animal> repository)
  {
    _repository = repository;
  }

  [HttpGet]
  public IActionResult GetAllAnimals()
  {
    var animals = _repository
      .GetAll()
      .Select(animal => new GetAnimalResponse { Name = animal.Name });
    return Ok(animals);
  }

  [HttpGet("{id}")]
  public IActionResult GetAnimalById(int id)
  {
    var animal = _repository.GetById(id);
    if (animal == null)
    {
      return NotFound();
    }
    return Ok(new GetAnimalResponse { Name = animal.Name });
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateAnimalRequest animalRequest)
  {
    var validationResults = await ValidateAsync(animalRequest);
    if (!validationResults.IsValid)
    {
      foreach (var error in validationResults.Errors)
      {
        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
      }
      return ValidationProblem(ModelState);
    }

    var newAnimal = new Animal { Name = animalRequest.Name! };
    _repository.Create(newAnimal);
    return CreatedAtAction(nameof(GetAnimalById), new { id = newAnimal.Id }, newAnimal);
  }

  [HttpPut("{id}")]
  public IActionResult UpdateEmployee(int id, [FromBody] UpdateAnimalRequest animalRequest)
  {
    var existingAnimal = _repository.GetById(id);
    if (existingAnimal == null)
    {
      return NotFound();
    }

    existingAnimal.Name = animalRequest.Name;

    _repository.Update(existingAnimal);
    return Ok(existingAnimal);
  }
}
