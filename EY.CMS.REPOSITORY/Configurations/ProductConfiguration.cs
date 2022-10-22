using EY.CMS.CORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.REPOSITORY.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductParentId).IsRequired();
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ProductNameEN).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.ProductNameRU).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.SeoUrl).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Image).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.BannerImage).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.Text).IsRequired(false).HasColumnType("text");
            builder.Property(x => x.TextEN).IsRequired(false).HasColumnType("text");
            builder.Property(x => x.TextRU).IsRequired(false);
        }
    }
}
