using System;

namespace ToDoApp.Api.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get;  set; }
        public string Email { get; set; }
        public DateTime AddedAt { get; set; }

        //public virtual ICollection<ToDoList> TodoLists { get; set; }
    }
}
