using TvMaze.Core.Constants;
using TvMaze.Core.DTO;
using TvMaze.Core.Entity;

namespace TvMaze.Core.Mappers
{
    public static class CastMapper
    {
        public static CastDTO Map(this Cast cast)
        {
            return cast != null
                ?
                new CastDTO
                {
                    Id = cast.Id,
                    Name = cast.Name,
                    Birthday = cast.Birthday?.ToString(FormatConstants.DateFormat).Replace("/", "-")
                }
                :
                null;
        }
    }
}
