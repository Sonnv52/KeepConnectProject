using MediatR;

namespace Chat.Application.Features.UserConnectionId.Requests.Commads
{
    public class RemoveUserconnectCommad : IRequest<bool>
    {
        public string? idConnection { get; set; }
    }
}
