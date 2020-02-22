using System;
using System.Collections.Generic;
using ToDoApp.Db.Exceptions;

namespace ToDoApp.Db.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        public DateTime AddedAt { get; private set; }

        public virtual ICollection<ToDoList> TodoLists { get; private set; }

        public User(Guid id, string username, string email, string hash, string salt, DateTime addedAt)
        {
            SetId(id);
            SetUsername(username);
            SetEmail(email);
            SetPassword(hash, salt);
            SetAddedAt(addedAt);
            TodoLists = new List<ToDoList>();
        }

        private void SetId(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new SocialAppException(nameof(Id), "The id of user can not be empty!");
            }

            Id = id;
        }

        private void SetUsername(string username)
        {
            if(string.IsNullOrEmpty(username)) // + regex
            {
                throw new SocialAppException(nameof(username), "Username can not be empty!");

            }

            Username = username;
        }

        public void SetPassword(string hash, string salt)
        {
            if(string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                throw new SocialAppException("password", "There was problem with your password!");
            }

            Hash = hash;
            Salt = salt;
        }

        private void SetEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new SocialAppException(nameof(email), "Email can not be null!");
            }

            Email = email;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            if(addedAt  == DateTime.MinValue)
            {
                throw new SocialAppException(nameof(addedAt), "There was problem with adding date!");
            }

            AddedAt = addedAt;
        }
    }
}
