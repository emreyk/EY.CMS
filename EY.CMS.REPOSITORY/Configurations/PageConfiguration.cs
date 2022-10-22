
using Ey.Cms.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EyCms.REPOSITORY.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SeoUrl).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MetaTitle).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Text).IsRequired(false).HasColumnType("text");
        }
    }
}
