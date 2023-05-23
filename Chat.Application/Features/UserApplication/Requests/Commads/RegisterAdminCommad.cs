using Chat.Application.DTOs.UserApp;
using Chat.Application.Respone;
using MediatR;

namespace Chat.Application.Features.UserApplication.Requests.Commads
{
    public record RegisterAdminCommad : IRequest<BaseCommandResponse<bool>>
    {
        public UserDTO? UserDTOUser { get; init; }
    }
}
