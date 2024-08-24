using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Context
{
    public class BookContext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
    }
}
