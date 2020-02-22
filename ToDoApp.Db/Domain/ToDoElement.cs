using System;
using ToDoApp.Db.Interfaces;

namespace ToDoApp.Db.Domain
{
    public class ToDoElement : IFinishable, ISearchable
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime AddedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }

        public Guid ToDoListId { get; private set; }
        public virtual ToDoList ToDoList { get; private set; }
    }
}
