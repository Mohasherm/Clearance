using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Clearance.Server.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Clearance>()
                .HasIndex(c => c.NationalNum)
                .IsUnique(true);    
            base.OnModelCreating(builder);
        }

        public DbSet<Direction> Directions { get; set; }
        public DbSet<Collage> Collages { get; set; }
        public DbSet<CollageDirection> CollageDirections { get; set; }
        public DbSet<Clearance> Clearances{ get; set; }

    }
}
