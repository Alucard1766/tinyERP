using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
