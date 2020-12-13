using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TvMaze.Core.Interfaces.Services;
using TvMaze.Core.Extensions;
using TvMaze.ViewModels.Request;
using TvMaze.Mappers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TvMaze.Controllers
{
    [ApiController]
    [Route("api/show")]
    public class ShowController
    {
        private readonly ILogger<ShowController> _logger;
        private readonly IShowService _showService;
        public ShowController(ILogger<ShowController> logger, IShowService showService)
        {
            _logger = logger;
            _showService = showService;
        }

        [HttpPost, Route("paginated-list")]
        public async Task<IActionResult> PaginatedList([FromBody] PaginatedQueryVM queryVM)
        {
            try
            {
                return new ObjectResult(await _showService.PaginatedList(queryVM.Map()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message());
                return new ObjectResult($"{nameof(PaginatedList)}-{nameof(ShowController)} request failed.") { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
