using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Constants;
using TvMaze.Core.Entity;

namespace TvMaze.Infrastructure.Data.DbMapping
{
    public static class ShowMap
    {
        public static ModelBuilder MapShows(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Show>();

            // Key
            entity.HasKey(k => k.Id);

            // Index
            entity.HasIndex(i => i.Name)
                  .IsUnique();

            // Relations
            entity.HasMany(p => p.Casts)
                  .WithOne(o => o.Show)
                  .HasForeignKey(f => f.ShowId);

            // Length
            entity.Property(p => p.Name).HasMaxLength(PropertyConstants.DEFAULT_LENGTH);

            return modelBuilder;
        }
    }
}
