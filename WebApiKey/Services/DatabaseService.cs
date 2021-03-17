using Microsoft.EntityFrameworkCore;
using WebApiKey.Models;
using WebApiKey.Models.Mapping;

namespace WebApiKey.Services
{
    public class DatabaseService : DbContext
    {
        public DbSet<Todo> Todo { get; set; }

        public DatabaseService(DbContextOptions<DatabaseService> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMapping());
            modelBuilder.ApplyConfiguration(new HashKeyMapping());
        }
    }
}
