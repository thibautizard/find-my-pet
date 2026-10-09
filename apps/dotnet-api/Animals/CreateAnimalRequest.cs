using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace TheAnimalsAPI.Animals;

public class CreateAnimalRequest
{
  [Required(AllowEmptyStrings = false)]
  public string? Name { get; set; }
}

public class CreateAnimalRequestValidator : AbstractValidator<CreateAnimalRequest>
{
  public CreateAnimalRequestValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
  }
}
