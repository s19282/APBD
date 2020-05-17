using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw11.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(d => d.IdPrescription);
            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.DueDate).IsRequired();
        }
    }
}
