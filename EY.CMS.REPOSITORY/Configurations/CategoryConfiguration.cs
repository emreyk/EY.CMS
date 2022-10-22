using Ey.Cms.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EY.CMS.REPOSITORY.Configurations
{
    public class CategoryConfiguration
    {
        public void Configure(EntityTypeBuilder<Blog_Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.Blog).WithOne(x => x.Blog_Category).HasForeignKey(x => x.Blog_CategoryId);
        }
    }
}
