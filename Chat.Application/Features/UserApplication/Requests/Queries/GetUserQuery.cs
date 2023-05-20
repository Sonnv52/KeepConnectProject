using Chat.Application.DTOs.UserApp;
using MediatR;

namespace Chat.Application.Features.UserApplication.Requests.Queries
{
    public class GetUserQuery : IRequest<UserDTO>
    {
    }
}
