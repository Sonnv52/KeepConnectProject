using Chat.Application.Exceptions;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Application.Features.UserConnectionId.Validator;
using Chat.Application.Persistence.Contracts;
using MediatR;

namespace Chat.Application.Features.UserConnectionId.Handlers.Commads
{
    public class RemoveUserconnectCommadHandler : IRequestHandler<RemoveUserconnectCommad, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserconnectCommadHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveUserconnectCommad request, CancellationToken cancellationToken)
        {
            if (request == null || request.idConnection == null )
            {
                throw new ArgumentNullException(nameof(request));
            }

            var validator = new RemoveUserConnectCommadValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var result = await _unitOfWork.UserConnectionIdRepository.RemoveAsync(request.idConnection);
            return result;
        }
    }
}
