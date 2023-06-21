using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Application.Helper.ErorMessages.ValidateErors;
using FluentValidation;

namespace Chat.Application.Features.UserConnectionId.Validator
{
    public class RemoveUserConnectCommadValidator: AbstractValidator<RemoveUserconnectCommad>
    {
        public RemoveUserConnectCommadValidator()
    {
        RuleFor(x => x.idConnection).MaximumLength(100).NotNull()
            .WithMessage($"Connection {ValidateError.STRING_TOO_LONG}");
    }
}
}
