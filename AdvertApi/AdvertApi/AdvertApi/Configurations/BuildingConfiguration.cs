using AdvertApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(b => b.IdBuilding);
            builder.Property(b => b.Street).HasMaxLength(100).IsRequired();
            builder.Property(b => b.StreetNumber).HasColumnType("int").IsRequired();
            builder.Property(b => b.City).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Height).HasColumnType("decimal(6, 2)");
        }
    }
}
