using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAOs
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public Guid RoomId { get; set; }
        [Required]
        public string? RoomName { get; set; }
        public Guid AdminId { get; set; }
        public virtual ICollection<UserRoom>? UserRooms { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
