using System;
using ToDoApp.Db.Exceptions;
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

        public ToDoElement(Guid id, Guid toDoListId, string title, DateTime addedAt)
        {
            SetId(id);
            SetListId(toDoListId);
            SetTitle(title);
            SetAddedAt(addedAt);
        }

        private void SetId(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ToDOAppException(nameof(Id), "The id of element can not be empty!");
            }

            Id = id;
        }

        public void SetListId(Guid toDoListId)
        {
            if (toDoListId == Guid.Empty)
            {
                throw new ToDOAppException(nameof(ToDoListId), "The id of list can not be empty!");
            }

            ToDoListId = toDoListId;
        }

        public void SetTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                throw new ToDOAppException(nameof(Title), "Title can not be empty!");
            }

            Title = title;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            if (addedAt == DateTime.MinValue)
            {
                throw new ToDOAppException(nameof(AddedAt), "AddedAt can not be empty!");
            }

            AddedAt = addedAt;
        }

        public void SetFinishedAt(DateTime? finishedAt)
        {
            FinishedAt = finishedAt;
        }
    }
}
