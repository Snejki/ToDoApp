using System;

namespace ToDoApp.Db.Exceptions
{
    public class SocialAppException : Exception
    {
        public string Name { get; set; }

        public SocialAppException(string name, string message) : base(message)
        {
            Name = name;
        }
    }
}
