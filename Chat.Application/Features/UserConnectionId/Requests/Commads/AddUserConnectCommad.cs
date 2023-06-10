using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserConnectionId.Requests.Commads
{
    public class AddUserConnectCommad : IRequest<Unit>
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();    
        public string? ConnectionHubId { get; set;}
        public string? UserId { get; set; }
    }
}
