using System;

namespace ToDoApp.Api.Dtos.User
{
    public class UserGetDto
    {
        /// <summary>
        /// ID of user
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get;  set; }
        /// <summary>
        /// Email of user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Time of creation
        /// </summary>
        public DateTime AddedAt { get; set; }
    }
}
