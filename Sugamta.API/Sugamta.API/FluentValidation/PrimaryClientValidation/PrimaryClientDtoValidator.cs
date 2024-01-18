using FluentValidation;
using Models.Models.DTOs.PrimaryClientDTOs;

namespace Sugamta.API.FluentValidation.PrimaryClientValidation
{
    public class PrimaryClientDtoValidator:AbstractValidator<PrimaryClientDto>
    {
        public PrimaryClientDtoValidator()
        {
            RuleFor(u => u.PrimaryClientEmail).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid Email Address");
        }

    }
}
