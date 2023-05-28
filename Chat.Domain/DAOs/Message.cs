using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAOs
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        [MaxLength(500)]
        public string? Content { get; set; }
        public DateTime Created { get; set; }
        public string? File { get; set; }
        public bool HasDeleted { get; set; }
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        public virtual Room? Room { get; set; }
    }
}
