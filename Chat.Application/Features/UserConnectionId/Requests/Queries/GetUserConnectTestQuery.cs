using Chat.Application.DTOs.UserConnectionId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserConnectionId.Requests.Queries
{
    public class GetUserConnectTestQuery : IRequest<IEnumerable<UsersConnectionDTO>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
