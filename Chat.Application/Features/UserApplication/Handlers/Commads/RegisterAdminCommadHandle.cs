using Chat.Application.DTOs.UserApp.Validator;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class RegisterAdminCommadHandle : IRequestHandler<RegisterAdminCommad, BaseCommandResponse<bool>>
    {
        private readonly IUserService _userService;

        public RegisterAdminCommadHandle(IUserService userService)
        {
            _userService= userService;
        }

        public async Task<BaseCommandResponse<bool>> Handle(RegisterAdminCommad request, CancellationToken cancellationToken)
        {
            if(request.UserDTOUser is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var validator = new UserDTOValidator();
            var validatorResult = await validator.ValidateAsync(request.UserDTOUser);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var result = await _userService.SignUpAsync(request.UserDTOUser);
            return result;
        }
    }
}
