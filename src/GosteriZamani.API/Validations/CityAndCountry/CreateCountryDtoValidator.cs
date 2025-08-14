using FluentValidation;
using GosteriZamani.API.Models.Country;

namespace GosteriZamani.API.Validations.CityAndCountry;

public class CreateCountryDtoValidator : AbstractValidator<CreateCountryDto>
{
    public CreateCountryDtoValidator()
    {
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(100).WithMessage("Country name must not exceed 100 characters.");
    }
}
