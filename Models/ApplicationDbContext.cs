using Microsoft.EntityFrameworkCore;
namespace PS4GamingApplication.Models

{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Game> games { get; set; }
        public DbSet<User> users { get; set; }
    }
}