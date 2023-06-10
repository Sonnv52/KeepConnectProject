using AutoMapper;
using Chat.Application.Exceptions;
using Chat.Application.Features.UserConnectionId.Requests.Commads;
using Chat.Application.Features.UserConnectionId.Validator;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Domain.DAOs.MongoDbEntities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Features.UserConnectionId.Handlers.Commads
{
    public class AddUserConnectCommadHandler : IRequestHandler<AddUserConnectCommad, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public AddUserConnectCommadHandler(IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUserConnectCommad request, CancellationToken cancellationToken)
        {
            var validator = new AddUserConnectCommadValidator();
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
            {
                throw new ValidationException(validatorResult);
            }

            var user = _contextAccessor?.HttpContext?.Items["User"] as UserApp;
            request.UserId = user?.Id;
            var userConnection = _mapper.Map<UserConnectionID>(request);
            _= await _unitOfWork.UserConnectionIdRepository.AddAsync(userConnection);
            return Unit.Value;
        }
    }
}
