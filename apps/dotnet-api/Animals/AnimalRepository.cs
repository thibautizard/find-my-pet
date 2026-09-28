using System;
using FindMyPet.Api.Abstractions;

namespace FindMyPet.Api;

public class AnimalRepository : IRepository<Animal>
{
  private readonly List<Animal> _animals =
  [
    new() { Id = 1, Name = "Simba" },
    new() { Id = 2, Name = "Nala" },
  ];

  public Animal? GetById(int id)
  {
    return _animals.SingleOrDefault(animal => animal.Id == id);
  }

  public IEnumerable<Animal> GetAll()
  {
    return _animals;
  }

  public void Create(Animal entity)
  {
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    entity.Id = _animals.Select(e => e.Id).DefaultIfEmpty(0).Max() + 1;
    _animals.Add(entity);
  }

  public void Update(Animal entity)
  {
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    var existingAnimal = GetById(entity.Id);
    if (existingAnimal != null)
    {
      existingAnimal.Name = entity.Name;
    }
  }

  public void Delete(Animal entity)
  {
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    _animals.Remove(entity);
  }
}
