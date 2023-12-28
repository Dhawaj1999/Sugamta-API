using FluentValidation;
using Models.Models.DTOs.UserLoginHistoryDTOs;

namespace Sugamta.API.FluentValidation.UserLoginHistoryValidation
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(e => e.Password).NotNull().WithMessage("Password is required");
        }
    }
}
