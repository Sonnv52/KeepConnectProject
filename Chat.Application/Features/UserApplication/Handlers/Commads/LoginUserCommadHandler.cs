using Chat.Application.DTOs.UserApp.Validator;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Models.UserApp;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class LoginUserCommadHandle : IRequestHandler<LoginUserCommad, BaseCommandResponse<AuthenticationModel>>
    {
        private readonly IUserService _userService;

        public LoginUserCommadHandle(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseCommandResponse<AuthenticationModel>> Handle(LoginUserCommad request, CancellationToken cancellationToken)
        {
            if (request.User is null)
                throw new ArgumentNullException(nameof(request.User));

            var validator = new LoginDTOValidator();
            var validatorResult = await validator.ValidateAsync(request.User);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var result = await _userService.SignInAsync(request.User, cancellationToken)
                .ConfigureAwait(false);

            return new BaseCommandResponse<AuthenticationModel>
            {
                Errors = null,
                Data = result,
                Success = true,
                Message = string.Empty
            };

        }
    }
}
