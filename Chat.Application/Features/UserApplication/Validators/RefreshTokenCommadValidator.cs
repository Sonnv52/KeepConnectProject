using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Helper.ErorMessages.ValidateErors;
using FluentValidation;

namespace Chat.Application.Features.UserApplication.Validators
{
    public class RefreshTokenCommadValidator : AbstractValidator<RefreshTokenCommad>
    {
        public RefreshTokenCommadValidator()
        {
            RuleFor(r => r.AccessToken)
                .NotNull().NotEmpty().WithMessage($"AccessToken {ValidateError.CANT_BE_NULL}");

            RuleFor(r => r.RefreshToken)
                .NotNull().NotEmpty().WithMessage($"RefreshToken  {ValidateError.CANT_BE_NULL}");
        }
    }
}
