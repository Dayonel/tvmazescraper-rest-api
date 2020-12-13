using System;
using TvMaze.Core.Entity.Base;

namespace TvMaze.Core.Entity
{
    public class Cast : EntityBase
    {
        public Cast(string name, DateTime? birthday, int showId)
        {
            Name = name;
            Birthday = birthday;
            ShowId = showId;
        }

        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual int ShowId { get; set; }
        public virtual Show Show { get; set; }
    }
}
