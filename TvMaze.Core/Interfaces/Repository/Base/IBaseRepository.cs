using System.Threading.Tasks;
using TvMaze.Core.Entity.Base;

namespace TvMaze.Core.Interfaces.Repository.Base
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<bool> AddAsync(T entity);
    }
}
