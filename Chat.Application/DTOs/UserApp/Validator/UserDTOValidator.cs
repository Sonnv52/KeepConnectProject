using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.UserApp.Validator
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull()
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull()
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit");

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password).WithMessage("Confirm password does not match password");
        }
    }
}
