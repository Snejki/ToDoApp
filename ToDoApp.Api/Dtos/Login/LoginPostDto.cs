using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.Login
{
    public class LoginPostDto
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required(ErrorMessage = "Username field is required")]
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
