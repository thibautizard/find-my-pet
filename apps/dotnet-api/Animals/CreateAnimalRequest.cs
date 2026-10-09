using System.ComponentModel.DataAnnotations;

namespace TheAnimalsAPI.Animals;

public class CreateAnimalRequest
{
  [Required(AllowEmptyStrings = false)]
  public string? Name { get; set; }
}
