
using Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Respository
{
  
    public class BaseServicesContext : DbContext
    {
        public BaseServicesContext(DbContextOptions<BaseServicesContext> options) : base(options)
        {

        }
        public virtual DbSet<CustommerMachine> CustomerMachine { get; set; }
        public virtual DbSet<Custommer> Custommers { get; set; }
        public virtual DbSet<SeUser> SeUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BVG_UAT");

            modelBuilder.Entity<SeUser>().ToTable("SE_USER", t => t.ExcludeFromMigrations());
        }


    }
  

}
