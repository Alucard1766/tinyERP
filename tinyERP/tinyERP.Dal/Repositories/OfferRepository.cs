
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public OfferRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
