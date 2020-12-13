using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;
using TvMaze.Infrastructure.Data.Repository.Base;

namespace TvMaze.Infrastructure.Data.Repository
{
    public class CastRepository : BaseRepository<Cast>, ICastRepository<Cast>
    {
        public CastRepository(TvMazeDbContext dbContext) : base(dbContext) { }
    }
}
