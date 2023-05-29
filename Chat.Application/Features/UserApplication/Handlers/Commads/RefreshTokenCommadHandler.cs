using Chat.Application.DTOs.UserApp.Validator;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Features.UserApplication.Validators;
using Chat.Application.Models.UserApp;
using Chat.Application.Respone;
using Chat.Application.Services.Abstractions;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class RefreshTokenCommadHandle : IRequestHandler<RefreshTokenCommad, BaseCommandResponse<AuthenticationModel>>
    {
        private readonly IUserService _userService;

        public RefreshTokenCommadHandle(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseCommandResponse<AuthenticationModel>> Handle(RefreshTokenCommad request,
            CancellationToken cancellationToken)
        {
            var validator = new RefreshTokenCommadValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult);

            var result = await _userService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

            return new BaseCommandResponse<AuthenticationModel>
            {
                Data = result,
                Success = true
            };
        }
    }
}
