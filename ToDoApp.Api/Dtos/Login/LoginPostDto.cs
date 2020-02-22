using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.Login
{
    public class LoginPostDto
    {
        [Required(ErrorMessage = "Username field is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
