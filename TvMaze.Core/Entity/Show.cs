using System.Collections.Generic;
using TvMaze.Core.Entity.Base;

namespace TvMaze.Core.Entity
{
    public class Show : EntityBase
    {
        public string Name { get; set; }

        public virtual List<Cast> Casts { get; set; }
    }
}
