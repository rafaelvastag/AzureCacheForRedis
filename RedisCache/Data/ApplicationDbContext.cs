using Microsoft.EntityFrameworkCore;
using RedisCache.Models;

namespace RedisCache.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories;
        public DbSet<RedisCache.Models.Category> Category { get; set; }
    }
}
