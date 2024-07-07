using IntegrationTests.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Database;

public class InMemoryContext(DbContextOptions<InMemoryContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryContext");
        }
    }
    
    public DbSet<Account> Accounts { get; set; }
}