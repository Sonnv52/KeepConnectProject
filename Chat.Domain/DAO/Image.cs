﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAO
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public Guid IdImage { get; set; }
        public string? Path { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        public virtual UserApp? UserApp { get; set; }
    }
}
