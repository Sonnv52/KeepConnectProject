namespace Chat.Api.Requests.Room
{
    public class CreateRoomRequest
    {
        public IList<string?>? IdPartners { get; set; }
        public DateTime? CreatTime { get; set; } = DateTime.Now;
        public string? RoomName { get; set; }
    }
}
