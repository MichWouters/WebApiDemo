namespace WebAPIDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Naam { get; set; } = default!;

        public string? Beschrijving { get; set; }

        public decimal Prijs { get; set; }

        public List<OrderLijn> Orderlijnen { get; set; } = default!;
    }
}