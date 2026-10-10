using FindMyPet.Api.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TheAnimalsAPI.Animals;

public class AnimalsController : BaseController
{
  private readonly IRepository<Animal> _repository;
  private readonly ILogger<AnimalsController> _logger;

  public AnimalsController(IRepository<Animal> repository, ILogger<AnimalsController> logger)
  {
    _repository = repository;
    _logger = logger;
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
    _logger.LogInformation("Updating animal with ID: {AnimalId}", id);
    var existingAnimal = _repository.GetById(id);
    if (existingAnimal == null)
    {
      _logger.LogWarning("Animal with ID {AnimalId} not found", id);
      return NotFound();
    }

    existingAnimal.Name = animalRequest.Name;

    try
    {
      _repository.Update(existingAnimal);
      _logger.LogInformation("Animal with ID: {AnimalId} successfully updated", id);
      return Ok(existingAnimal);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occured while updating the animal with ID: {AnimalId}", id);
      return StatusCode(500, "An error occured while updating the animal");
    }
  }
}
