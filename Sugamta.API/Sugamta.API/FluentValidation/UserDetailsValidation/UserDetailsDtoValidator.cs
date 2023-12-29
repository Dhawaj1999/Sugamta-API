using FluentValidation;
using Sugamta.API.DTOs.UserDetailsDTOs;

namespace Sugamta.API.FluentValidation.UserDetailsValidation
{
    public class UserDetailsDtoValidator : AbstractValidator<UserDetailsDto>
    {
        public UserDetailsDtoValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(u => u.Address).NotNull().MaximumLength(50);
            RuleFor(u => u.State).NotNull().MaximumLength(50);
            RuleFor(u => u.Country).NotNull().MaximumLength(50);
            RuleFor(u => u.PhoneNumber).NotNull().MaximumLength(50);
            RuleFor(u => u.CreationDate).NotEmpty().WithMessage("CreationDate is required");
            RuleFor(u => u.UpdationDate).NotEmpty().WithMessage("UpdationDate is required");
        }
    }
}
