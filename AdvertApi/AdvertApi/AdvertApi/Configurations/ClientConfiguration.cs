using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.IdClient);
            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired().HasAnnotation("EmailAddress","");
            builder.Property(c => c.Phone).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Login).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Password).IsRequired().HasAnnotation("PasswordPropertyText", "");
        }
    }
}
