using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Repositories
{
    public interface IToDoListRepository
    {
        Task<ToDoList> GetById(Guid id);
        Task<int> CountPages(Guid userId, string searchPhrase, bool? isFinished, int pageSize);
        Task<ICollection<ToDoList>> GetForUser(Guid userId, string searchPhrase, bool? isFinished, int page, int pageSize);

        Task<bool> Add(ToDoList toDoList);
        Task<bool> Update(ToDoList toDoList);
        Task<bool> Remove(ToDoList toDoList);
    }
}
