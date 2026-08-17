using WebApiDemo.Data;
using WebAPIDemo.Models;
using WebAPIDemo.Repositories;

namespace WebApiDemo.Repositories
{
    public class BestellingRepository : GenericRepository<Bestelling>, IBestellingRepository
    {
        public BestellingRepository(WebAPIDemoContext context): base(context)
        {
        }
    }
}
