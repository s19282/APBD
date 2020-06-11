using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(b => b.IdAdvertisment);
            builder.Property(b => b.Name).HasColumnType("int");
            builder.Property(b => b.Price).HasColumnType("decimal(6, 2)").IsRequired();
            builder.Property(b => b.Area).HasColumnType("decimal(6, 2)").IsRequired();
            builder.Property(b => b.IdCampaign).HasAnnotation("ForeignKey","IdCampaign");
        }
    }
}
