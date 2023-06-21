using Chat.Application.Features.Rooms.Requests.Commads;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using FluentValidation;
using Chat.Application.Helper.ErorMessages.ValidateErors;

namespace Chat.Application.Features.UserConnectionId.Validator
{
    public class AddUserConnectCommadValidator : AbstractValidator<AddUserConnectCommad>
    {
        public AddUserConnectCommadValidator()
        {
            RuleFor(x => x.ConnectionHubId).MaximumLength(100)
                .WithMessage($"Connection {ValidateError.STRING_TOO_LONG}");
            RuleFor(x => x.UserId).NotNull().WithMessage($"UserId {ValidateError.CANT_BE_NULL}");
        }
    }
}
