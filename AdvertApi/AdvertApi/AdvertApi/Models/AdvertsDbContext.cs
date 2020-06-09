using AdvertApi.Configurations;
using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;


namespace AdvertApi.Model
{
    public class AdvertsDbContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        public AdvertsDbContext() { }
        public AdvertsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BuildingConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignConfiguration());
            
            modelBuilder.Entity<Campaign>()
                .HasMany(c => c.Banners)
                .WithOne(b => b.Campaign);
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Campaigns)
                .WithOne(c=> c.Client);
            modelBuilder.Entity<Building>()
                .HasMany(b => b.CampaignsFrom)
                .WithOne(c => c.FBulidling);
            modelBuilder.Entity<Building>()
                .HasMany(b => b.CampaignsTo)
                .WithOne(c => c.TBulidling);
        }
    }
}
