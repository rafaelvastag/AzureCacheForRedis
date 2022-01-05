
using Microsoft.EntityFrameworkCore;
using RedisCache.Function.Models;

namespace RedisCache.Function
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
    }
}
