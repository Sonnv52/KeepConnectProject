using Chat.Application.Queries;
using Chat.Core.DAO;
using MediatR;

namespace Chat.Application.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return new UserDTO
            {
                FullName = "Hello"
            };
        }
    }
}
