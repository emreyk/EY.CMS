
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace EY.CMS.REPOSITORY
{
    public class EyCmsContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EyCmsContext(DbContextOptions<EyCmsContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Referance> Referances { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Blog_Category> Blog_Categorys { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
