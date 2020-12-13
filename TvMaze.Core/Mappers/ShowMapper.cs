using System.Linq;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;

namespace TvMaze.Core.Mappers
{
    public static class ShowMapper
    {
        public static ShowDTO Map(this Show show)
        {
            return show != null
                ?
                new ShowDTO
                {
                    Id = show.Id,
                    Name = show.Name,
                    Cast = show.Casts.Select(s => s.Map()).ToList()
                }
                : 
                null;
        }
    }
}
