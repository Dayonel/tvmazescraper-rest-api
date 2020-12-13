using TvMaze.Core.DTO;
using TvMaze.ViewModels.Request;

namespace TvMaze.Mappers
{
    public static class PaginatedQueryMapper
    {
        public static PaginatedQueryDTO Map(this PaginatedQueryVM queryVM)
        {
            return queryVM != null
                ?
                new PaginatedQueryDTO(queryVM.Page, queryVM.PageSize)
                :
                null;
        }
    }
}
