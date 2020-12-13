using Newtonsoft.Json;

namespace TvMaze.Infrastructure.Scraper.Model
{
    public class ShowModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
