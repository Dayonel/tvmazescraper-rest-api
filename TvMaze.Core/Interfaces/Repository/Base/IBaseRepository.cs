using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TvMaze.Core.Entity.Base;

namespace TvMaze.Core.Interfaces.Repository.Base
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }
}
