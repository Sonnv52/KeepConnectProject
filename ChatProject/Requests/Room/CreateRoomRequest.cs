namespace Chat.Api.Requests.Room
{
    public class CreateRoomRequest
    {
        public IList<Guid?>? IdMember { get; set; }
        public DateTime? CreatTime { get; set; } = DateTime.Now;
    }
}
