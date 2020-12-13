using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Constants;
using TvMaze.Core.Entity;

namespace TvMaze.Infrastructure.Data.DbMapping
{
    public static class CastMap
    {
        public static ModelBuilder MapCasts(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Cast>();

            // Key
            entity.HasKey(k => k.Id);

            // Relations
            entity.HasOne(p => p.Show)
                  .WithMany(o => o.Casts)
                  .HasForeignKey(f => f.ShowId);

            // Length
            entity.Property(p => p.Name).HasMaxLength(PropertyConstants.DEFAULT_LENGTH);

            return modelBuilder;
        }
    }
}
