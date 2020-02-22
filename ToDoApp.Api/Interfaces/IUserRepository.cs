using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<ICollection<User>> GetAll();

        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Remove(User user);
    }
}
