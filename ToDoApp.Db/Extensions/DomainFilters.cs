using System;
using System.Linq;
using ToDoApp.Db.Interfaces;

namespace ToDoApp.Db.Extensions
{
    public static class DomainFilters
    {
        public static IQueryable<T> FilterByUserId<T>(this IQueryable<T> query, Guid userId) where T: IUserId
        {
            return query.Where(q => q.UserId == userId);
        }

        public static IQueryable<T> SearchByTitle<T>(this IQueryable<T> query, string title) where T : ISearchable
        {
            if(!string.IsNullOrEmpty(title))
            {
                query = query.Where(q => q.Title.ToLower().Contains(title.ToLower()));
            }

            return query;
        }

        public static IQueryable<T> FilterByFinishedStatus<T>(this IQueryable<T> query, bool? isFinished) where T : IFinishable
        {
            if(isFinished.HasValue)
            {
                query = isFinished.Value ? 
                    query.Where(q => q.FinishedAt.HasValue) 
                    : query.Where(q => !q.FinishedAt.HasValue);
            }

            return query;
        }
    }
}
