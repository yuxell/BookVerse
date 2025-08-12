using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.ViewModels.Login
{
    public class UserCreate_VM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9.*\-_]+$", ErrorMessage = "Password can only contain letters, numbers, and the characters . * - _")]
        [StringLength(12, ErrorMessage = "Min 5,Max 12 characters....", MinimumLength = 5)]
        [Compare("PasswordAgain", ErrorMessage = "Passwords do not match")]
        public string Password { get; set; }
        [Required]
        public string PasswordAgain { get; set; }
    }
}
