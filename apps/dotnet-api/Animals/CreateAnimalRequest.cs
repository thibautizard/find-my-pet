using System.ComponentModel.DataAnnotations;

namespace TheAnimalsAPI.Animals;

public class CreateAnimalRequest
{
  [Required]
  public required string Name { get; set; }
}
