using System.ComponentModel.DataAnnotations;

namespace TvMaze.ViewModels.Request
{
    public class PaginatedQueryVM
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid page.")]
        public int Page { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid page size.")]
        public int PageSize { get; set; }
    }
}
