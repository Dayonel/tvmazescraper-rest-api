using System.Threading.Tasks;
using TvMaze.Core.Entity.Base;
using TvMaze.Core.Interfaces.Repository.Base;

namespace TvMaze.Infrastructure.Data.Repository.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        private readonly TvMazeDbContext _dbContext;
        public BaseRepository(TvMazeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
