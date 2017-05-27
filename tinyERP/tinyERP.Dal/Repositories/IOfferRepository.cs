using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<Offer> GetOffersWithDocumentsByOrderId(int orderId);
    }
}
