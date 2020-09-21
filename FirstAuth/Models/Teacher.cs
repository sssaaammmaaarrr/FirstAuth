using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAuth.Models
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string name { get; set; }
        public string subject { get; set; }

        public int age { get; set; }

        public virtual IdentityUser TUser{ get; set; }
        public string TUserId { get; set; }
    }
}
