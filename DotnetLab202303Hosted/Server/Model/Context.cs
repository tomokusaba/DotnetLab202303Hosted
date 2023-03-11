using Microsoft.EntityFrameworkCore;

namespace DotnetLab202303Hosted.Server.Model
{
    public class Context : DbContext
    {
        //public Context(DbContextOptions<Context> options) :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=dotnetLab;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<ParsonItem> PersonItems { get; set; } = null!;
        public DbSet<SkillItem> SkillItems { get; set; } = null!;
    }
}
