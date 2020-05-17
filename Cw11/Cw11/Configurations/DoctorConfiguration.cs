using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw11.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.IdDoctor);
            builder.Property(d => d.FirstName).HasMaxLength(150).IsRequired();
            builder.Property(d => d.LastName).HasMaxLength(200).IsRequired();
            builder.Property(d => d.Email).HasMaxLength(100).IsRequired().HasAnnotation("EmailAddress", "");
        }
    }
}
