using Microsoft.EntityFrameworkCore;
using Labb1_OOP.Modeller;

namespace Labb1_OOP.Data;

public class SpelCenterDbContext : DbContext
{
    public DbSet<Spel> Spel { get; set; }
    public DbSet<Medlem> Medlem { get; set; }
    public DbSet<Bokning> Bokning { get; set; }
}
