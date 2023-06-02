using Chat.Application.Exceptions;
using Chat.Application.Features.Rooms.Requests.Commads;
using Chat.Application.Features.Rooms.Validators;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Features.Rooms.Handlers.Commads
{
    public class CreateRoomCommadHandler : IRequestHandler<CreateRoomCommad, Guid>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        public CreateRoomCommadHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }
        public async Task<Guid> Handle(CreateRoomCommad request, CancellationToken cancellationToken)
        {

            var validator = new CreateRoomCommadValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult);

            var creater = _contextAccessor?.HttpContext?.Items["User"] as UserApp;
            
            if (creater == null)
                throw new ArgumentNullException(nameof(creater), "User is null.");

            request.IdPartners!.Add(creater.Id);
            var room = new Room
            {
                AdminId = creater.Id ?? throw new ArgumentNullException(nameof(creater.Id), "AdminId can't be null."),
                RoomId = Guid.NewGuid(),
                RoomName = request.RoomName ?? null,
            };

            foreach(var IdPartner in request.IdPartners)
            {
                var partner = await _unitOfWork.UserRepository.GetByStringIdAsync(IdPartner ?? "");

                if (partner == null)
                {
                    throw new ArgumentException("Invalid partner id: " + IdPartner);
                }

                var roomUser = new UserRoom
                {
                    Id = Guid.NewGuid(),
                    Room = room,
                    RoomId = room.RoomId,
                    UserApp = partner,
                    UserId = partner.Id
                };
               await _unitOfWork.UserRoomRepository.AddAsync(roomUser);
            }
           
            await _unitOfWork.CommitAsync();
            return room.RoomId;
        }
    }
}
