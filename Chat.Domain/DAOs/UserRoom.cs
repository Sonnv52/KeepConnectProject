using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAOs
{
    [Table("UserRoom")]
    public class UserRoom
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        [Required]
        [ForeignKey("UserApp")]
        public string? UserId { get; set; }
        public virtual Room? Room { get; set; }
        public virtual UserApp? UserApp { get; set; }
    }
}
