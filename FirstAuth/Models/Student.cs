using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAuth.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string StudentName { get; set; }
        public int Age { get; set; }

        public virtual IdentityUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
