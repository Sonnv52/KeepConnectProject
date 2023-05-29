namespace Chat.Api.Requests.UserRequests
{
    public class GetFriendsRequest
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string? SearchKey { get; set; }
    }
}
