using System;

namespace ToDoApp.Db.Exceptions
{
    public class ToDOAppException : Exception
    {
        public string Name { get; set; }

        public ToDOAppException(string name, string message) : base(message)
        {
            Name = name;
        }
    }
}
