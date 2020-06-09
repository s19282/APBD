using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(c => c.IdCampaign);
            builder.Property(c => c.StartDate).IsRequired().HasColumnType("Date");
            builder.Property(c => c.EndDate).IsRequired().HasColumnType("Date");
            builder.Property(c => c.PricePreSquareMeter).IsRequired().HasColumnType("decimal(5, 2)");
            //builder.Property(c => c.IdClient).HasAnnotation("ForeignKey", "IdClient");
            //builder.Property(c => c.FromIdBuilding).HasAnnotation("ForeignKey", "FromIdBuilding");
            //builder.Property(c => c.ToIdBuilding).HasAnnotation("ForeignKey", "ToIdBuilding");
        }
    }
}
