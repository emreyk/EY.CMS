
using Ey.Cms.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EyCms.REPOSITORY.Configurations
{
    public class ReferanceConfiguration : IEntityTypeConfiguration<Referance>
    {
        public void Configure(EntityTypeBuilder<Referance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Image).IsRequired().HasColumnType("text");

        }
    }
}
