﻿using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Api.Dtos.User
{
    public class UserPostDto
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Email of user
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
