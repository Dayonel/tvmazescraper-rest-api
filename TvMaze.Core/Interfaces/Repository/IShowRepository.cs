using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;
using TvMaze.Core.Entity.Base;
using TvMaze.Core.Interfaces.Repository.Base;

namespace TvMaze.Core.Interfaces.Repository
{
    public interface IShowRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        Task<List<Show>> PaginatedList(PaginatedQueryDTO queryDTO);
    }
}
