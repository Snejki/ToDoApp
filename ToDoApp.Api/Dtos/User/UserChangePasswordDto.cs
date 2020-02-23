using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.User
{
    public class UserChangePasswordDto
    {
        /// <summary>
        /// Currenct passsword
        /// </summary>
        [Required]
        public string CurrentPassword { get; set; }
        /// <summary>
        /// New password
        /// </summary>
        [Required]
        public string NewPassword { get; set; }
    }
}
