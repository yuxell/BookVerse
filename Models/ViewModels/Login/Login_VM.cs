using System.ComponentModel.DataAnnotations;

namespace BookWebApp.Models.ViewModels.Login
{
    public class Login_VM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
