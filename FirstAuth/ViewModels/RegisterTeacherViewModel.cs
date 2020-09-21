using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAuth.ViewModels
{
    public class RegisterTeacherViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [ DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="they are dont match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(20)]
        public string name { get; set; }
        public string subject { get; set; }

        public int age { get; set; }

    }
}
