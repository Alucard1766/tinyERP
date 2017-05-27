using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public OfferRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Offer> GetOffersWithDocumentsByOrderId(int orderId)
        {
            return TinyErpContext.Offers.Where(o => o.OrderId == orderId).Include(o => o.Document).ToList();
        }
    }
}
