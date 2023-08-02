using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public List<Book> GetBooksFromDatabase()
        {
            return Books.ToList();
        }
    }
}
