using System.Collections.Generic;
using System.Threading.Tasks;
using TvMaze.Core.DTO;

namespace TvMaze.Core.Interfaces.Services
{
    public interface IShowService
    {
        Task<List<ShowDTO>> PaginatedList(PaginatedQueryDTO queryDTO);
    }
}
