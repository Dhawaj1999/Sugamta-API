using FluentValidation;
using Models.Models;

namespace Sugamta.API.FluentValidation.UserLoginHistoryValidation
{
    public class UserLoginHistoryValidator : AbstractValidator<UserLoginHistory>
    {
        public UserLoginHistoryValidator()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(e => e.LastLoginTime).NotNull().WithMessage("Last login time is required");
            RuleFor(i => i.RoleId).NotNull().WithMessage("Role Id is required");
            RuleFor(r => r.RoleType).NotNull().WithMessage("Role type is required");
        }

    }
}
