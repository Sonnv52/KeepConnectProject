using Chat.Application.DTOs.UserApp;
using MediatR;

namespace Chat.Application.Features.UserApplication.Requests.Queries
{
    public record GetUserQuery : IRequest<UserDTO>
    {
    }
}
