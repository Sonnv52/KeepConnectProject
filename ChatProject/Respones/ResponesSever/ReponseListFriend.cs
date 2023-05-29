using Chat.Api.Respones.Common;
using Chat.Application.DTOs.UserApp;
using Chat.Application.Helper.Extentions;

namespace Chat.Api.Respones.ResponesSever
{
    public class ReponseListFriend : ResponseBase<PagedList<FriendToList>>
    {
        public int Total { get; set; }
    }
}
