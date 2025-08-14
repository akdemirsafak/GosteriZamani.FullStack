using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GosteriZamani.API.Filters;
public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ValidationFilter> _logger;

    public ValidationFilter(IServiceProvider serviceProvider, ILogger<ValidationFilter> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 1) Model binding hatalarını (JSON parse vb) önce yakala
        if (!context.ModelState.IsValid)
        {
            _logger.LogDebug("ModelState geçersiz - binding hatası.");
            context.Result = new BadRequestObjectResult(context.ModelState);
            return;
        }

        // 2) Action argümanlarını dolaş ve uygun validator'ı bul
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
            var validator = _serviceProvider.GetService(validatorType);
            _logger.LogDebug("Checking validator for {Type} - found: {Has}", arg.GetType().Name, validator != null);
            if (validator == null) continue;

            // non-generic IValidator'ı kullanarak çağır (daha güvenli)
            if (validator is FluentValidation.IValidator nonGenericValidator)
            {
                var validationContext = new FluentValidation.ValidationContext<object>(arg);
                var validationResult = await nonGenericValidator.ValidateAsync(validationContext);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors
                        .Select(e => new { e.PropertyName, e.ErrorMessage });
                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }
            else
            {
                _logger.LogWarning("Bulunan validator, IValidator değil: {Type}", validator.GetType().FullName);
            }
        }

        await next();
    }
}