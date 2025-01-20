using E_commerce.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Configrtion
{
    public class ProudectConfigrtion : IEntityTypeConfiguration<Proudect>
    {
        public void Configure(EntityTypeBuilder<Proudect> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(200).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P=>P.Brand).WithMany().HasForeignKey(P=>P.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(P=>P.Type).WithMany().HasForeignKey(P=>P.TypeId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
