using FluentValidation;
using Models.Models.DTOs.UserLoginHistoryDTOs;

namespace Sugamta.API.FluentValidation.UserLoginHistoryValidation
{
    public class CreateUserLoginHistoryDtoValidator : AbstractValidator<CreateUserLoginHistoryDto>
    {
        public CreateUserLoginHistoryDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(e => e.LastLoginTime).NotNull().WithMessage("Last login time is required");
            RuleFor(i => i.RoleId).NotNull().WithMessage("Role Id is required");
            RuleFor(r => r.RoleType).NotNull().WithMessage("Role type is required");
        }
    }
}
