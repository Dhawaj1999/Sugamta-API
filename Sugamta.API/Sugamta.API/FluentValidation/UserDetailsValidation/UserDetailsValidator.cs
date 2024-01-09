﻿using FluentValidation;
using Models.Models;

namespace Sugamta.API.FluentValidation.UserDetailsValidation
{
    public class UserDetailsValidator : AbstractValidator<UserDetails>
    {
        public UserDetailsValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(u => u.Address).NotNull().MaximumLength(50);
            RuleFor(u => u.State).NotNull().MaximumLength(50);
            RuleFor(u => u.Country).NotNull().MaximumLength(50);
            RuleFor(u => u.PhoneNumber).NotNull().MaximumLength(50);
        }

    }
}
