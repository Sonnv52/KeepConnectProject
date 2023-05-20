using Chat.Application.DTOs.UserApp;
using Chat.Application.DTOs.UserApp.Validator;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Persistence.Contracts;
using Chat.Application.Respone;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class RegisterAdminCommadHandle : IRequestHandler<RegisterAdminCommad, BaseCommandResponse<bool>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterAdminCommadHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            var result = await _userRepository.SignUpAsync(request.UserDTOUser);
            return result;
        }
    }
}
