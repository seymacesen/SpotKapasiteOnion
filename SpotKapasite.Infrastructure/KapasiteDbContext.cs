using Microsoft.EntityFrameworkCore;
using SpotKapasite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotKapasite.Infrastructure
{
    public class KapasiteDbContext : DbContext
    {
        public KapasiteDbContext(DbContextOptions<KapasiteDbContext> options) : base(options) { }

        public DbSet<Kapasite> Kapasiteler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kapasite>(entity =>
            {
                entity.HasKey(k => k.Id);

                // KapasiteMiktari için hassasiyet ve ölçek
                entity.Property(k => k.KapasiteMiktari)
                      .HasPrecision(18, 2); // 18 basamak, 2 ondalık basamak

                // Fiyat için hassasiyet ve ölçek
                entity.Property(k => k.Fiyat)
                      .HasPrecision(18, 2); // 18 basamak, 2 ondalık basamak
            });
        }


    }
}
