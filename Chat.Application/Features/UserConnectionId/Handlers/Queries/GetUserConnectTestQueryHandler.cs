using Chat.Application.DTOs.UserConnectionId;
using Chat.Application.Features.UserConnectionId.Requests.Queries;
using Chat.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserConnectionId.Handlers.Queries
{
    public class GetUserConnectTestQueryHandler : IRequestHandler<GetUserConnectTestQuery, IEnumerable<UsersConnectionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserConnectTestQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UsersConnectionDTO>> Handle(GetUserConnectTestQuery request, CancellationToken cancellationToken)
        {
            var usersConnect = await _unitOfWork.UserConnectionIdRepository.GetAllAsync();
            var result = usersConnect.Select(x => new UsersConnectionDTO
            {
                ConnectionId = x.ConnectionHubId,
                UserId = x.UserId
            });

            return result;
        }
    }
}
