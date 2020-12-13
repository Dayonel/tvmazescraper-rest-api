using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;
using TvMaze.Core.Settings;
using TvMaze.Infrastructure.Data.Repository.Base;

namespace TvMaze.Infrastructure.Data.Repository
{
    public class ShowRepository : BaseRepository<Show>, IShowRepository<Show>
    {
        private readonly TvMazeDbContext _dbContext;
        private readonly PaginationSettings _paginationSettings;
        public ShowRepository(TvMazeDbContext dbContext, PaginationSettings paginationSettings) : base(dbContext)
        {
            _dbContext = dbContext;
            _paginationSettings = paginationSettings;
        }

        public async Task<List<Show>> PaginatedList(PaginatedQueryDTO queryDTO)
        {
            var pageSize = queryDTO.PageSize != default
                ? queryDTO.PageSize
                : _paginationSettings.PageSize;

            var page = queryDTO.Page != default
                ? queryDTO.Page
                : _paginationSettings.DefaultPage;

            return await _dbContext.Show
                .Include(i => i.Casts)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
