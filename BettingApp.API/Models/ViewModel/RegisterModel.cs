using System.ComponentModel.DataAnnotations;

namespace BettingApp.API.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set;}

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set;}

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
