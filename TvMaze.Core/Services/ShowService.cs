using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;
using TvMaze.Core.Interfaces.Services;
using TvMaze.Core.Mappers;

namespace TvMaze.Core.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository<Show> _showRepository;
        public ShowService(IShowRepository<Show> showRepository)
        {
            _showRepository = showRepository;
        }

        public async Task<List<ShowDTO>> PaginatedList(PaginatedQueryDTO queryDTO)
        {
            return (await _showRepository.PaginatedList(queryDTO)).Select(s => s.Map()).ToList();
        }
    }
}
