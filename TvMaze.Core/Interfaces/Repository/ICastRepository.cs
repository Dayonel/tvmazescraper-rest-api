using TvMaze.Core.Entity.Base;
using TvMaze.Core.Interfaces.Repository.Base;

namespace TvMaze.Core.Interfaces.Repository
{
    public interface ICastRepository<T> : IBaseRepository<T> where T : EntityBase { }
}
