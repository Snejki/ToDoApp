using System;

namespace ToDoApp.Db.Interfaces
{
    public interface IFinishable
    {
        public DateTime? FinishedAt { get; }
    }
}
