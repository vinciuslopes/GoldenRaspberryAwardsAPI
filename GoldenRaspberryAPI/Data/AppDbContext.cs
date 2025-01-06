using GoldenRaspberryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
