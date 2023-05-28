using Chat.Api.Respones.Common;

namespace Chat.Api.Respones.ResponesSever
{
    public class ResponeToken : ResponseBase<string>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
