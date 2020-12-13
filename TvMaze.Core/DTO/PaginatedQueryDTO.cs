namespace TvMaze.Core.DTO
{
    public class PaginatedQueryDTO
    {
        public PaginatedQueryDTO(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
