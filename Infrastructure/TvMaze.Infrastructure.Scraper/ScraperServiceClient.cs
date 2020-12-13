using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TvMaze.Core.Constants;
using TvMaze.Core.DTO.Base;
using TvMaze.Core.Entity;
using TvMaze.Core.Interfaces.Repository;
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
        private readonly IShowRepository<Show> _showRepository;
        private readonly ICastRepository<Cast> _castRepository;
        public ScraperServiceClient(IHttpClientFactory httpClientFactory, ScraperSettings scraperSettings,
            ILogger<ScraperServiceClient> logger, IShowRepository<Show> showRepository, ICastRepository<Cast> castRepository)
        {
            _httpClientFactory = httpClientFactory;
            _scraperSettings = scraperSettings;
            _logger = logger;
            _showRepository = showRepository;
            _castRepository = castRepository;
        }

        public async Task ExecuteScraping()
        {
            var client = _httpClientFactory.CreateClient(HttpClientConstants.THROTTLED_HTTP_CLIENT);
            client.DefaultRequestHeaders.Add(HttpClientConstants.USER_AGENT_HEADER, HttpClientConstants.USER_AGENT_UNIQUE_ID);

            var page = 0;
            var dbShows = await _showRepository.CountAsync();
            if (dbShows > 0)
                page = (int)Math.Floor((double)dbShows / _scraperSettings.PageSize);

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
                {
                    _logger.LogInformation("Scraping finished successfully.");
                    return;
                }

                foreach (var show in shows)
                {
                    var showEntity = await AddShow(show);

                    var getCastsResult = await GetCast(show.Id, client);
                    if (!getCastsResult.Success)
                    {
                        _logger.LogCritical(getCastsResult.ErrorMessage);
                        return;
                    }

                    var casts = getCastsResult.Data;
                    foreach (var cast in casts)
                        await AddCast(showEntity.Id, cast);
                }

                page++;
            }
        }

        private async Task AddCast(int showId, CastModel cast)
        {
            var birthdayParse = DateTime.TryParseExact(cast.Person.Birthday, FormatConstants.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday)
                ? birthday : (DateTime?)null;

            var castEntity = new Cast(cast.Person.Name, birthdayParse, showId);

            if (!await _castRepository.AnyAsync(a => a.ShowId == showId && a.Name == cast.Person.Name))
                await _castRepository.AddAsync(castEntity);
        }

        private async Task<Show> AddShow(ShowModel show)
        {
            var showEntity = new Show(show.Name);

            if (!await _showRepository.AnyAsync(a => a.Name == show.Name))
                await _showRepository.AddAsync(showEntity);
            else
                showEntity = await _showRepository.GetAsync(g => g.Name == show.Name);

            return showEntity;
        }

        private async Task<BaseDTO<List<ShowModel>>> GetShows(int page, HttpClient client)
        {
            var result = new BaseDTO<List<ShowModel>>();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{_scraperSettings.ApiBaseUrl}/{_scraperSettings.ShowsRoute.Replace("{page}", page.ToString())}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NotFound)
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
