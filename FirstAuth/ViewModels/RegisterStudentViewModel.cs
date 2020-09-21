using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAuth.ViewModels
{
    public class RegisterStudentViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="the password and confirmation password are not match")]
        public string confirmPassword { get; set; }

        [Required]
        [MaxLength(20)]
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
}
