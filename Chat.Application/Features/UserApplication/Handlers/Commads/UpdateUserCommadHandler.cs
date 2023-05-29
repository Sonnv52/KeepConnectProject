using AutoMapper;
using Chat.Application.DTOs.UserApp.Validator;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserApplication.Requests.Commads;
using Chat.Application.Persistence.Contracts;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Commads
{
    public class UpdateUserComadHandle : IRequestHandler<UpdateUserCommad, Unit>
    {
        private readonly IMapper _mapper;

        public UpdateUserComadHandle(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommad request, CancellationToken cancellationToken)
        {
            /* if (request.User == null)
             {
                 return Unit.Value;
             }

             var validator = new UserDTOValidator();
             var validatorResult = await validator.ValidateAsync(request.User);

             if (!validatorResult.IsValid)
             {
                 throw new ValidationException(validatorResult);
             }

             var user = await _userRepository.GetAsync(Guid.NewGuid());
             if (user == null)
             {
                 throw new DirectoryNotFoundException(nameof(user));
             }
             _mapper.Map(request.User, user);
             string test = GetName();
             await _userRepository.UpdateAsync(user);
             return Unit.Value;*/
            return Unit.Value;
        }
        private string GetName()
        {
            return string.Empty;
        }
    }
}
