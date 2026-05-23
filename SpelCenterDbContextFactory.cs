using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Labb1_OOP.Data;
public class SpelCenterDbContextFactory : IDesignTimeDbContextFactory<SpelCenterDbContext>
{
    public SpelCenterDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SpelCenterDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;Database=SpelCenterDb;Trusted_Connection=True;TrustServerCertificate=True;"
        );

        return new SpelCenterDbContext(optionsBuilder.Options);
    }
}