namespace WebAPIDemo.Models
{
    public class Bestelling
    {
        public int Id { get; set; }

        public int KlantId { get; set; }

        public Klant? Klant { get; set; } = default!;

        public List<OrderLijn> Orderlijnen { get; set; } = default!;
    }
}