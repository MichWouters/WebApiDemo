namespace WebAPIDemo.Data
{
    public class WebAPIDemoContext : DbContext
    {
        public WebAPIDemoContext(DbContextOptions<WebAPIDemoContext> options)
            : base(options) { }

        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<Bestelling> Bestellingen { get; set; }

        public DbSet<Klant> Klanten { get; set; }

        public DbSet<OrderLijn> OrderLijnen { get; set; }

        public DbSet<Product> Producten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            GenerateTables(modelBuilder);
            SeedData(modelBuilder);
        }

        private void GenerateTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Laptop>(entity =>
            {
                entity.ToTable("Laptops");
                entity.Property(p => p.Merk).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Processor).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RamInGB).IsRequired();
                entity.Property(p => p.Prijs).IsRequired().HasPrecision(18, 2);
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
                .WithMany(x => x.OrderLijnen)
                .HasForeignKey(y => y.BestellingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                entity.HasOne(p => p.Product)
                .WithMany(x => x.Orderlijnen)
                .HasForeignKey(y => y.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 1. Klanten Seeden
            modelBuilder.Entity<Klant>().HasData(
                new Klant { Id = 1, Naam = "Van Der Neffe", Voornaam = "Leon", AangemaaktDatum = new DateTime(2022, 10, 1) },
                new Klant { Id = 2, Naam = "Van De Kasseinen", Voornaam = "Firmin", AangemaaktDatum = new DateTime(2022, 10, 2) },
                new Klant { Id = 3, Naam = "Kiekeboe", Voornaam = "Marcel", AangemaaktDatum = new DateTime(2022, 10, 3) }
            );

            // 2. Producten Seeden
            // Let op de 'm' achter de prijzen om expliciet aan te geven dat het om decimalen gaat.
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Naam = "fiets", Prijs = 100.00m, Beschrijving = "Dit is een fiets" },
                new Product { Id = 2, Naam = "koersfiets", Prijs = 200.00m, Beschrijving = "Dit is een mooie koersfiets" },
                new Product { Id = 3, Naam = "auto", Prijs = 2000.00m, Beschrijving = "Dit is een auto" }
            );

            // 3. Bestellingen Seeden
            // We geven enkel de Foreign Key (KlantId) mee, niet het volledige Klant-object.
            modelBuilder.Entity<Bestelling>().HasData(
                new Bestelling { Id = 1, KlantId = 1 },
                new Bestelling { Id = 2, KlantId = 2 },
                new Bestelling { Id = 3, KlantId = 3 },
                new Bestelling { Id = 4, KlantId = 2 },
                new Bestelling { Id = 5, KlantId = 3 }
            );

            // 4. OrderLijnen Seeden
            modelBuilder.Entity<OrderLijn>().HasData(
                new OrderLijn { Id = 1, Aantal = 3, BestellingId = 1, ProductId = 1 },
                new OrderLijn { Id = 2, Aantal = 7, BestellingId = 1, ProductId = 2 },
                new OrderLijn { Id = 3, Aantal = 4, BestellingId = 2, ProductId = 1 },
                new OrderLijn { Id = 4, Aantal = 1, BestellingId = 2, ProductId = 2 },
                new OrderLijn { Id = 5, Aantal = 2, BestellingId = 3, ProductId = 1 },
                new OrderLijn { Id = 6, Aantal = 3, BestellingId = 4, ProductId = 1 },
                new OrderLijn { Id = 7, Aantal = 1, BestellingId = 4, ProductId = 3 },
                new OrderLijn { Id = 8, Aantal = 2, BestellingId = 5, ProductId = 1 },
                new OrderLijn { Id = 9, Aantal = 6, BestellingId = 5, ProductId = 2 },
                new OrderLijn { Id = 10, Aantal = 10, BestellingId = 5, ProductId = 3 }
            );

            //5. Laptops Seeden
            modelBuilder.Entity<Laptop>().HasData(
                new Laptop { Id = 1, Merk = "Apple", Processor = "M3 Pro", RamInGB = 18, Prijs = 2499.00, GPU = "Apple M3 Pro 18-core" },
                new Laptop { Id = 2, Merk = "Dell", Processor = "Intel Core i7-13700H", RamInGB = 16, Prijs = 1299.99, GPU = "NVIDIA RTX 4060" },
                new Laptop { Id = 3, Merk = "Lenovo", Processor = "AMD Ryzen 7 7840HS", RamInGB = 16, Prijs = 999.50, GPU = "AMD Radeon 780M" },
                new Laptop { Id = 4, Merk = "ASUS", Processor = "Intel Core i9-13900H", RamInGB = 32, Prijs = 2199.00, GPU = "NVIDIA RTX 4080" },
                new Laptop { Id = 5, Merk = "HP", Processor = "Intel Core i5-1335U", RamInGB = 8, Prijs = 649.00, GPU = "Intel Iris Xe" },
                new Laptop { Id = 6, Merk = "Acer", Processor = "AMD Ryzen 5 7520U", RamInGB = 8, Prijs = 499.00, GPU = "AMD Radeon 610M" },
                new Laptop { Id = 7, Merk = "MSI", Processor = "Intel Core i7-14700HX", RamInGB = 32, Prijs = 1899.99, GPU = "NVIDIA RTX 4070" },
                new Laptop { Id = 8, Merk = "Apple", Processor = "M3", RamInGB = 8, Prijs = 1299.00, GPU = "Apple M3 10-core" },
                new Laptop { Id = 9, Merk = "Lenovo", Processor = "Intel Core Ultra 7 155H", RamInGB = 32, Prijs = 1649.00, GPU = "Intel Arc Graphics" },
                new Laptop { Id = 10, Merk = "ASUS", Processor = "AMD Ryzen 9 7945HX", RamInGB = 64, Prijs = 3299.00, GPU = "NVIDIA RTX 4090" },
                new Laptop { Id = 11, Merk = "HP", Processor = "AMD Ryzen 7 7730U", RamInGB = 16, Prijs = 849.00, GPU = "AMD Radeon Graphics" },
                new Laptop { Id = 12, Merk = "Dell", Processor = "Intel Core i5-1235U", RamInGB = 8, Prijs = 579.00, GPU = "Intel UHD Graphics" },
                new Laptop { Id = 13, Merk = "Razer", Processor = "Intel Core i9-14900HX", RamInGB = 32, Prijs = 3599.00, GPU = "NVIDIA RTX 4090" },
                new Laptop { Id = 14, Merk = "Samsung", Processor = "Intel Core Ultra 5 125H", RamInGB = 16, Prijs = 1199.00, GPU = "Intel Arc Graphics" },
                new Laptop { Id = 15, Merk = "Microsoft", Processor = "Intel Core i7-1255U", RamInGB = 16, Prijs = 1499.00, GPU = "Intel Iris Xe" },
                new Laptop { Id = 16, Merk = "Acer", Processor = "Intel Core i7-13620H", RamInGB = 16, Prijs = 1099.00, GPU = "NVIDIA RTX 4050" },
                new Laptop { Id = 17, Merk = "Gigabyte", Processor = "Intel Core i7-13700H", RamInGB = 16, Prijs = 1399.00, GPU = "NVIDIA RTX 4060" },
                new Laptop { Id = 18, Merk = "Apple", Processor = "M3 Max", RamInGB = 36, Prijs = 3999.00, GPU = "Apple M3 Max 30-core" },
                new Laptop { Id = 19, Merk = "Lenovo", Processor = "AMD Ryzen 3 7320U", RamInGB = 8, Prijs = 429.00, GPU = "AMD Radeon 610M" },
                new Laptop { Id = 20, Merk = "HP", Processor = "Intel Core Ultra 7 155H", RamInGB = 16, Prijs = 1349.00, GPU = "Intel Arc Graphics" }
            );
        }
    }
}