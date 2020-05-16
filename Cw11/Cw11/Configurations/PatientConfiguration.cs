using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw11.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(d => d.IdPatient);
            builder.Property(d => d.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(d => d.LastName).HasMaxLength(200).IsRequired();
            builder.Property(d => d.BirdthDate).IsRequired();
        }
    }
}
