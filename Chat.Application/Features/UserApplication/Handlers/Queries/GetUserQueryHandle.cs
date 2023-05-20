using Chat.Application.DTOs.UserApp;
using Chat.Application.Features.UserApplication.Requests.Queries;
using MediatR;

namespace Chat.Application.Features.UserApplication.Handlers.Queries
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
