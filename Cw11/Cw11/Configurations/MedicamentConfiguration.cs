using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw11.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(d => d.IdMedicament);
            builder.Property(d => d.Name).HasMaxLength(100).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(200).IsRequired();
            builder.Property(d => d.Type).HasMaxLength(100).IsRequired();
        }
    }
}
