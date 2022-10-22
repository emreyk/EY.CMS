
using Ey.Cms.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ey.Cms.REPOSITORY.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Keyword).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Descripton).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Phone1).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.Phone2).IsRequired(false).HasMaxLength(20);
            builder.Property(x => x.Address).IsRequired(false).HasColumnType("text");
            builder.Property(x => x.Facebook).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Twitter).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Instagram).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Youtube).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.Logo).IsRequired(true).HasColumnType("text");

        }
    }
}
