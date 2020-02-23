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
    public class ElementRepository : IElementRepository
    {
        private readonly ToDoAppContext _context;
        public ElementRepository(ToDoAppContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ToDoElement element)
        {
            _context.ToDoElements.Add(element);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<ToDoElement>> GetForUser(Guid userId, string phrase, bool? isFinished)
        {
            return await _context
                .ToDoElements
                .Where(e => e.ToDoList.UserId == userId)
                .SearchByTitle(phrase)
                .FilterByFinishedStatus(isFinished)
                .ToListAsync();
        }

        public async Task<ToDoElement> GetById(Guid id)
        {
            return await _context.ToDoElements.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> Remove(ToDoElement element)
        {
            _context.Remove(element);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(ToDoElement element)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
