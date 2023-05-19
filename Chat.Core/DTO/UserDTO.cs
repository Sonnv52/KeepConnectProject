using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.DAO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Adress { get; set; }
    }
}
