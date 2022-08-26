using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class ApplactionDbContext : DbContext
    {

        public ApplactionDbContext(DbContextOptions<ApplactionDbContext> options) :base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
