using Chat.Application.Respone;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Chat.Application.Features.UserApplication.Requests.Commads
{
    public record UpLoadAvatarCommad : IRequest<BaseCommandResponse<IList<string>>>
    {
        public IFormFile? Avatar { get; set; }
    }
}
