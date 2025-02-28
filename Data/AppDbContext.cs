using Microsoft.EntityFrameworkCore;
using WB_labb3_API_new_.Models;

namespace WB_labb3_API_new_.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Experience> Experiences { get; set; }
    }
}