using FluentValidation;
using FluentValidation.Results;

namespace PortalAdmin.Validators
{
    public class PropertyValidator<TModel>
    {
        private readonly IValidator<TModel> _validator;

        public PropertyValidator(IValidator<TModel> validator)
        {
            _validator = validator;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var context = ValidationContext<TModel>.CreateWithOptions((TModel)model, x => x.IncludeProperties(propertyName));
            ValidationResult result = await _validator.ValidateAsync(context);
            return result.IsValid
                ? []
                : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
