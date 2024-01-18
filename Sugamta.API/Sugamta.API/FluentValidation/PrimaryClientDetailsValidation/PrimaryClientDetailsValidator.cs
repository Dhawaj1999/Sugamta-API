using FluentValidation;
using Models.Models;

namespace Sugamta.API.FluentValidation.PrimaryClientDetailsValidation
{
    public class PrimaryClientDetailsValidator:AbstractValidator<PrimaryClientDetails>
    {
        public PrimaryClientDetailsValidator()
        {
            RuleFor(u => u.PrimaryClientEmail).EmailAddress();
            //RuleFor(u=>u.Address).NotEmpty();
        }
    }
}
