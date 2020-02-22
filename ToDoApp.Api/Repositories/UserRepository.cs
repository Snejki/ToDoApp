using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Api.Interfaces;
using ToDoApp.Db;
using ToDoApp.Db.Domain;

namespace ToDoApp.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoAppContext _context;

        public UserRepository(ToDoAppContext context)
        {
            _context = context; // throw 
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }           

        public async Task<bool> Add(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Remove(User user)
        {
            _context.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(User user)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
