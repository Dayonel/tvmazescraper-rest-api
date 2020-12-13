using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TvMaze.Core.Constants;
using TvMaze.Core.DTO.Base;
using TvMaze.Core.Interfaces.ServiceClients;
using TvMaze.Core.Settings;
using TvMaze.Infrastructure.Extensions;
using TvMaze.Infrastructure.Scraper.Model;
using TvMaze.Infrastructure.Scraper.Model.Cast;

namespace TvMaze.Infrastructure.Scraper
{
    public class ScraperServiceClient : IScraperServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ScraperSettings _scraperSettings;
        private readonly ILogger<ScraperServiceClient> _logger;
        public ScraperServiceClient(IHttpClientFactory httpClientFactory, ScraperSettings scraperSettings, 
            ILogger<ScraperServiceClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _scraperSettings = scraperSettings;
            _logger = logger;
        }

        public async Task ExecuteScraping()
        {
            var page = 0;
            var client = _httpClientFactory.CreateClient(HttpClientConstants.THROTTLED_HTTP_CLIENT);
            client.DefaultRequestHeaders.Add(HttpClientConstants.USER_AGENT_HEADER, HttpClientConstants.USER_AGENT_UNIQUE_ID);

            while (true)
            {
                var getShowsResult = await GetShows(page, client);
                if (!getShowsResult.Success)
                {
                    _logger.LogCritical(getShowsResult.ErrorMessage);
                    return;
                }

                var shows = getShowsResult.Data;
                if (shows == null || !shows.Any())
                    return;

                foreach (var show in shows)
                {
                    var getCastResult = await GetCast(show.Id, client);
                    if (!getCastResult.Success)
                    {
                        _logger.LogCritical(getCastResult.ErrorMessage);
                        return;
                    }
                }

                page++;
            }
        }

        private async Task<BaseDTO<List<ShowModel>>> GetShows(int page, HttpClient client)
        {
            var result = new BaseDTO<List<ShowModel>>();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_scraperSettings.ApiBaseUrl}/{_scraperSettings.ShowsRoute.Replace("{page}", page.ToString())}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                result.AddError(await response.Content.ReadAsStringAsync());
                return result;
            }

            var content = await response.Content.ReadAsStringAsync();
            var deserializeResult = content.TryDeserialize<List<ShowModel>>();
            if (!deserializeResult.Success)
            {
                result.AddError(deserializeResult.ErrorMessage);
                return result;
            }

            result.Data = deserializeResult.Data;

            return result;
        }

        private async Task<BaseDTO<List<CastModel>>> GetCast(int showId, HttpClient client)
        {
            var result = new BaseDTO<List<CastModel>>();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_scraperSettings.ApiBaseUrl}/{_scraperSettings.CastRoute.Replace("{id}", showId.ToString())}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                result.AddError(await response.Content.ReadAsStringAsync());
                return result;
            }

            var content = await response.Content.ReadAsStringAsync();
            var deserializeResult = content.TryDeserialize<List<CastModel>>();
            if (!deserializeResult.Success)
            {
                result.AddError(deserializeResult.ErrorMessage);
                return result;
            }

            result.Data = deserializeResult.Data;

            return result;
        }
    }
}
