namespace WebAPIDemo.Models
{
    public class OrderLijn
    {
        public int Id { get; set; }

        public double Aantal { get; set; }

        public int BestellingId { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; } = default!;

        public Bestelling? Bestelling { get; set; } = default!;
    }
}