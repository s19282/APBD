using AdvertApi.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Model
{
    public class AdvertsDbContext : DbContext
    {
        public DbSet<Building> Buildings { get; set; }

        public AdvertsDbContext() { }
        public AdvertsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BuildingConfiguration());
        }
    }
}
