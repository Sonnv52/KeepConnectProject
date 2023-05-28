using Chat.Application.DTOs.UserApp;
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
    public class LoginUserCommad : IRequest<BaseCommandResponse<AuthenticationModel>>
    {
        public LoginDTO? User { get; set; }
        public DateTime? CallAt { get; set; } = DateTime.Now;
    }
}
