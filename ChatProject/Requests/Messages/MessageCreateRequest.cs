namespace Chat.Api.Requests.Messages
{
    public class MessageCreateRequest 
    {

        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? RoomName { get; set; }
        //public IFormFile? formFile { get; set; }
    }
}
