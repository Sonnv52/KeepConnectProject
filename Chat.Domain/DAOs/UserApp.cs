using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.DAOs
{
    public class UserApp : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }
        [MaxLength(100)]
        public string? Adress { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual IList<Image?>? Images { get; set; }
        public virtual IList<Avatar?>? Avatars { get; set; }
        public virtual ICollection<UserRoom>? UserRooms { get; set; }
    }
}
