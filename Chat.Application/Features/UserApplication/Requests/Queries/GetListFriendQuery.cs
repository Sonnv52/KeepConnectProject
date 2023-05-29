using Chat.Application.DTOs.UserApp;
using Chat.Application.Helper.Extentions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Features.UserApplication.Requests.Queries
{
    public class GetListFriendQuery : IRequest<PagedList<FriendToList>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string? SearchKey { get; set; }
    }
}
