using IntegrationTests.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Database;

public class InMemoryDatabaseTests
{
    private readonly InMemoryContext _context;

    public InMemoryDatabaseTests()
    {
        _context = CreateContext();
        SeedData(_context);
    }

    private static InMemoryContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<InMemoryContext>()
            .UseInMemoryDatabase("InMemoryContext")
            .Options;
        
        return new InMemoryContext(options);
    }
    
    private static void SeedData(InMemoryContext context)
    {
        var accounts = new List<Account>
        {
            new()
            {
                Name = "Test Account 1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Test Account 2",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
        
        context.Accounts.AddRange(accounts);
        context.SaveChanges();
    }

    [Fact]
    public void TestReadData()
    {
        var accounts = _context.Accounts.ToList();
        Assert.Equal(2, accounts.Count);
    }

    [Fact]
    public void TestAddData()
    {
        var newAccount = new Account
        {
            Name = "Test Account 3",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();

        var accounts = _context.Accounts.ToList();
        Assert.Equal(3, accounts.Count);
    }

    [Fact]
    public void TestUpdateData()
    {
        var account = _context.Accounts.First();
        account.Name = "Updated Name";
        _context.SaveChanges();

        var updatedAccount = _context.Accounts.First();
        Assert.Equal("Updated Name", updatedAccount.Name);
    }

    [Fact]
    public void TestDeleteData()
    {
        var account = _context.Accounts.First();
        _context.Accounts.Remove(account);
        _context.SaveChanges();

        var accounts = _context.Accounts.ToList();
        Assert.Equal(1, accounts.Count);
    }

    private void Dispose()
    {
        DestroyContext(_context);
    }

    private static void DestroyContext(InMemoryContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}