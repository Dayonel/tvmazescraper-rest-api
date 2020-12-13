using Newtonsoft.Json;

namespace TvMaze.Infrastructure.Scraper.Model.Cast
{
    public class CastModel
    {
        [JsonProperty("person")]
        public CastPersonModel Person { get; set; }
    }
}
