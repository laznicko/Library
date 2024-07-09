using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Library.DataLayer;
using Library.DataLayer.Entities;

public class TestDatabaseFixture : IDisposable
{
    public DBContext Context { get; private set; }

    public TestDatabaseFixture()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DBContext>()
            .UseSqlServer("Server=localhost;Database=library;User=root;Password=root;")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        Context = new DBContext(options);

        // Ensure the database is created
        Context.Database.EnsureCreated();

        // Initialize the database with test data
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // Clear existing data
        Context.Books.RemoveRange(Context.Books);
        Context.SaveChanges();

        // Add test data
        Context.Books.AddRange(new List<BookEntity>
        {
            new BookEntity { BookTitle = "Test Book 1", ISBN = "1111111111" },
            new BookEntity { BookTitle = "Test Book 2", ISBN = "2222222222" }
        });
        Context.SaveChanges();
    }

    public void Dispose()
    {
        // Clean up test data
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
