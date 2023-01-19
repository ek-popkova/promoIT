using promoit_backend_cs_api.Data;

namespace promoit_backend_cs.Services
{
    public class ExistsService
    {
        private readonly promo_itContext _context;

        public ExistsService(promo_itContext db)
        {
            _context = db;
        }

        public bool BcrExists(int id)
        {
            return _context.Bcrs.Any(e => e.Id == id);
        }

        public bool CampaignExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public bool NPRExists(int id)
        {
            return _context.NonProfitRepresentatives.Any(e => e.Id == id);
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public bool ProductToCampaignExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }

}
