using Chat.Application.Features.UserApplication.Requests.Commads;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserApplication.Validators
{
    public class RefreshTokenCommadValidator : AbstractValidator<RefreshTokenCommad>
    {
        public RefreshTokenCommadValidator()
        {
            RuleFor(r => r.AccessToken)
                .NotNull().NotEmpty().WithMessage("AccessToken Not Null");

            RuleFor(r => r.RefreshToken)
                .NotNull().NotEmpty().WithMessage("RefreshToken Not Null");
        }
    }
}
