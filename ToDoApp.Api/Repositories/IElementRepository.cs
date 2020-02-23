using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Repositories
{
    public interface IElementRepository
    {
        Task<ToDoElement> GetById(Guid id);
        Task<ICollection<ToDoElement>> GetForUser(Guid userId, string phrase, bool? isFinished);

        Task<bool> Add(ToDoElement element);
        Task<bool> Update(ToDoElement element);
        Task<bool> Remove(ToDoElement element);
    }
}
