using System;
using System.Collections.Generic;
using ToDoApp.Db.Exceptions;
using ToDoApp.Db.Interfaces;

namespace ToDoApp.Db.Domain
{
    public class ToDoList : IUserId, IFinishable, ISearchable, IEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime AddedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public string Color { get; private set; }
        public virtual ICollection<ToDoElement> ToDoElements { get; private set; }


        public virtual User User { get; private set; }
        public Guid UserId { get; private set; }

        public ToDoList(Guid id, Guid userId, string title, string color, DateTime addedAt)
        {
            SetId(id);
            SetUserId(userId);
            SetTitle(title);
            SetColor(color);
            SetAddedAt(addedAt);
            ToDoElements = new List<ToDoElement>();
        }

        private void SetId(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ToDOAppException(nameof(Id), "The id of list can not be empty!");
            }

            Id = id;
        }

        private void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ToDOAppException(nameof(UserId), "The id of user can not be empty!");

            }

            UserId = userId;
        }

        public void SetTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                throw new ToDOAppException(nameof(Title), "Title can not be empty!");
            }

            Title = title;
        }

        public void SetColor(string color)
        {
            if(string.IsNullOrEmpty(color))
            {
                throw new ToDOAppException(nameof(Color), "Color can not be empty!");
            }

            Color = color;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            if(addedAt == DateTime.MinValue)
            {
                throw new ToDOAppException(nameof(AddedAt), "AddedAt not be empty!");
            }

            AddedAt = addedAt;
        }

        public void SetFinishedAt(DateTime? finishedAt)
        {
            FinishedAt = finishedAt;
        }

    }
}
