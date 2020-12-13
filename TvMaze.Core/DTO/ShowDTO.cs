using System.Collections.Generic;

namespace TvMaze.Core.DTO
{
    public class ShowDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CastDTO> Cast { get; set; }
    }
}
