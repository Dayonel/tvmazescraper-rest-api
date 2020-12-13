using Newtonsoft.Json;

namespace TvMaze.Infrastructure.Scraper.Model.Cast
{
    public class CastPersonModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
    }
}
