using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(d => d.IdPrescription);
            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.DueDate).IsRequired();
            builder.Property(d => d.IdPatient).IsRequired();
            builder.Property(d => d.IdDoctor).IsRequired();
        }
    }
}
