using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw11.Configurations
{
    public class Prescription_MedicamentConfiguration : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
        {
            builder.HasKey(d => d.IdMedicament);
            builder.HasKey(d => d.IdPrescription);
            builder.Property(d => d.Dose);
            builder.Property(d => d.Details).HasMaxLength(100).IsRequired();
        }
    }
}
