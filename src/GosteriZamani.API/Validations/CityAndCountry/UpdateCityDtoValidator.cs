using FluentValidation;
using GosteriZamani.API.Models.City;

namespace GosteriZamani.API.Validations.CityAndCountry;

public class UpdateCityDtoValidator : AbstractValidator<UpdateCityDto>
{
    public UpdateCityDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("City ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("City name is required.")
            .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");
        RuleFor(x => x.CountryId)
            .NotEmpty().WithMessage("Country ID is required.");
    }
}
