using AutoMapper;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Application.Features.UserConnectionId.Validator;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Domain.DAOs.MongoDbEntities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Features.UserConnectionId.Handlers.Commads
{
    public class AddUserConnectCommadHandler : IRequestHandler<AddUserConnectCommad, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserConnectCommad> _logger;

        public AddUserConnectCommadHandler(IUnitOfWork unitOfWork,
            IMapper mapper, ILogger<AddUserConnectCommad> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(AddUserConnectCommad request, CancellationToken cancellationToken)
        {
            var validator = new AddUserConnectCommadValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var userConnection = _mapper.Map<UserConnectionID>(request);
            await _unitOfWork.UserConnectionIdRepository.AddAsync(userConnection);
            return Unit.Value;
        }
    }
}
