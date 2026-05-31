using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models;

namespace WebApiDemo.Data
{
    public class WebAPIDemoContext : DbContext
    {
        public WebAPIDemoContext(DbContextOptions<WebAPIDemoContext> options)
            : base(options) { }

        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<Bestelling> Bestellingen { get; set; }

        public DbSet<Klant> Klanten { get; set; }

        public DbSet<OrderLijn>OrderLijnen { get; set; }

        public DbSet<Product> Producten{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Laptop>(entity =>
            {
                entity.ToTable("Laptops");
                entity.Property(p => p.Merk).IsRequired().HasMaxLength(50); // Voorkomt nvarchar(max)
                entity.Property(p => p.Processor).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RamInGB).IsRequired(); // RAM past makkelijk in een 'tinyint' (0-255), bespaart database-ruimte
                entity.Property(p => p.Prijs).IsRequired().HasPrecision(18, 2); // Zorgt voor een decimal(18,2) in SQL Server voor nauwkeurige geldbedragen
                entity.Property(p => p.GPU).IsRequired().HasMaxLength(100);

            });

            modelBuilder.Entity<Bestelling>(entity =>
            {
                entity.ToTable("Bestelling");

                entity.HasOne(p => p.Klant)
                .WithMany(x => x.Bestellingen)
                .HasForeignKey(y => y.KlantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            });

            modelBuilder.Entity<Klant>(entity =>
            {
                entity.ToTable("Klant");
            });

            modelBuilder.Entity<OrderLijn>(entity =>
            {
                entity.ToTable("OrderLijn");

                entity.HasOne(p => p.Bestelling)
                .WithMany(x => x.Orderlijnen)
                .HasForeignKey(y => y.BestellingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                entity.HasOne(p => p.Bestelling)
                .WithMany(x => x.Orderlijnen)
                .HasForeignKey(y => y.BestellingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
            });
        }
    }
}