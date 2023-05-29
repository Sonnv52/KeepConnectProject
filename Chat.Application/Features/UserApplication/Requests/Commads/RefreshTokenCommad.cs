using Chat.Application.Models.UserApp;
using Chat.Application.Respone;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserApplication.Requests.Commads
{
    public class RefreshTokenCommad : IRequest<BaseCommandResponse<AuthenticationModel>>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
