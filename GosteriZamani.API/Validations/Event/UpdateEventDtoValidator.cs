using FluentValidation;
using GosteriZamani.API.Models.Event;

namespace GosteriZamani.API.Validations.Event;

public class UpdateEventDtoValidator : AbstractValidator<UpdateEventDto>
{
    public UpdateEventDtoValidator() 
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Etkinlik ID'si boş olamaz.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Etkinlik adı boş olamaz.")
            .MaximumLength(100).WithMessage("Etkinlik adı en fazla 100 karakter olabilir.");
        
        RuleFor(x => x.Detail)
            .NotEmpty().WithMessage("Etkinlik detayı boş olamaz.")
            .MaximumLength(500).WithMessage("Etkinlik detayı en fazla 500 karakter olabilir.");
        
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Etkinlik adresi boş olamaz.")
            .MaximumLength(200).WithMessage("Etkinlik adresi en fazla 200 karakter olabilir.");
        
        RuleFor(x => x.CityId)
            .NotEmpty().WithMessage("Şehir ID'si boş olamaz.");
        
        RuleFor(x => x.Organizer)
            .MaximumLength(100).WithMessage("Organizatör adı en fazla 100 karakter olabilir.");
        
        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("En az bir kategori seçilmelidir.")
            .Must(categoryIds => categoryIds.Count > 0).WithMessage("Kategori ID'leri boş olamaz.");
    }
}
