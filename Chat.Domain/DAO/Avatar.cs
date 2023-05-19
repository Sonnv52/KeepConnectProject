﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.DAO
{
    [Table("Avatar")]
    public class Avatar
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Path is required")]
        public string? Path { get; set; }
        public DateTime? Created { get; set; }
        public virtual UserApp? UserApp { get; set; }
    }
}