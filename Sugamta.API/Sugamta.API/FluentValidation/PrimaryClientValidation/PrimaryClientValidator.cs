using FluentValidation;
using Models.Models;

namespace Sugamta.API.FluentValidation.PrimaryClientValidation
{
    public class PrimaryClientValidator:AbstractValidator<PrimaryClient>
    {
        public PrimaryClientValidator()
        {
            RuleFor(u => u.PrimaryClientEmail).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
        }
    }
}
