using FluentValidation;
using Models.Models;
using Sugamta.API.DTOs.UserDTOs;
using System.Text.RegularExpressions;

namespace Sugamta.API.FluentValidation.UserValidation
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            //  RuleFor(u => u.UserID);
            RuleFor(u => u.Email)
                  .NotEmpty().WithMessage("Email is required.")
                  .NotNull().WithMessage("Email cannot be null.")
                  .EmailAddress().WithMessage("Invalid email address format. Please use a valid email address.");
            /*.Must(BeValidEmailFormat).WithMessage("Invalid email . Use a valid email.....!");
*/

            //  RuleFor(u => u.Name).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull()
           .NotEmpty().WithMessage("Password cannot be empty")
           .MinimumLength(8).WithMessage("Password length should be at least 8 characters")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
           .Matches("[0-9]").WithMessage("Password must contain at least one digit")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
            RuleFor(u => u.CreationDate).NotNull().NotEmpty();
           
            RuleFor(u => u.CreatedBy).NotNull().NotEmpty();
        }
        /* private bool BeValidEmailFormat(string email)
         {
             // Custom email domain validation logic
             var regex = new Regex(@"^[a-zA-Z0-9_.+-]+@gmail\.com$");


             return regex.IsMatch(email);
         }
 */
    }
}
