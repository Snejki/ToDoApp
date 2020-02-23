using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Db;
using ToDoApp.Db.Domain;
using ToDoApp.Db.Extensions;

namespace ToDoApp.Api.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoAppContext _context;

        public ToDoListRepository(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ToDoList toDoList)
        {
            _context.ToDoLists.Add(toDoList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountPages(Guid userId, string searchPhrase, bool? isFinished, int pageSize)
        {
            return (await _context
                .ToDoLists
                .FilterByUserId(userId)
                .SearchByTitle(searchPhrase)
                .FilterByFinishedStatus(isFinished)
                .CountAsync() + pageSize - 1) / pageSize;
        }

        public async Task<ToDoList> GetById(Guid id)
        {
            return await _context.ToDoLists.SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<ICollection<ToDoList>> GetForUser(Guid userId, string searchPhrase, bool? isFinished, int page, int pageSize)
        {
            return await _context
                .ToDoLists
                .FilterByUserId(userId)
                .SearchByTitle(searchPhrase)
                .FilterByFinishedStatus(isFinished)
                .Paginate(page, pageSize)
                .ToListAsync();        
        }

        public async Task<bool> Remove(ToDoList toDoList)
        {
            _context.Remove(toDoList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(ToDoList toDoList)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
