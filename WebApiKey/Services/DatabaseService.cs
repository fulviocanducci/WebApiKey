using Microsoft.EntityFrameworkCore;
using WebApiKey.Models;

namespace WebApiKey.Services
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Todo> Todo { get; set; }
    }
}
