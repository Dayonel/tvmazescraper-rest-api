using System;
using TvMaze.Core.Entity.Base;

namespace TvMaze.Core.Entity
{
    public class Cast : EntityBase
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public virtual int ShowId { get; set; }
        public virtual Show Show { get; set; }
    }
}
