﻿using FluentValidation;
using MyApp.Application.DTOs.Users;

namespace MyApp.Application.Validators.Users
{
    public class UserCodeValidationValidator : AbstractValidator<UserCodeValidationRequest>
    {
        public UserCodeValidationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo Email es obligatorio.")
                .EmailAddress().WithMessage("El formato del campo email no es válido.");

            RuleFor(x => x.CodeValidation)
                .NotEmpty().WithMessage("El campo CodeValidation es obligatorio.")
                .Length(5).WithMessage("El campo CodeValidation debe tener exactamente 5 dígitos.")
                .Matches(@"^\d{5}$").WithMessage("El campo CodeValidation debe contener solo números.");
        }
    }
}
