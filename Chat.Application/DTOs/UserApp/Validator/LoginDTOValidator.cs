using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.DTOs.UserApp.Validator
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(u => u.UserName)
              .NotEmpty().WithMessage("User Name is required")
              .NotNull()
              .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
              .EmailAddress().WithMessage("Invalid email address");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull()
                .Matches(@"^\S*$").WithMessage("Password must not contain whitespace");
        }
    }
}
