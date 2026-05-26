using Microsoft.EntityFrameworkCore;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.Data;

public class SpelCenterDbContext : DbContext
{
    public DbSet<Spel> Spel { get; set; }
    public DbSet<Medlem> Medlem { get; set; }
    public DbSet<Bokning> Bokning { get; set; }

    public SpelCenterDbContext(
        DbContextOptions<SpelCenterDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bokning>()
            .HasMany(b => b.anmalda)
            .WithMany(m => m.bokningar);

        modelBuilder.Entity<Bokning>()
            .HasMany(b => b.bokadeSpel)
            .WithMany(s => s.bokningar);

        modelBuilder.Entity<Bokning>()
            .HasOne(b => b.ansvarig)
            .WithMany(m => m.ansvaradeBokningar)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
