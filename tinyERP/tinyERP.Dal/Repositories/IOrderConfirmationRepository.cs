using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface IOrderConfirmationRepository : IRepository<OrderConfirmation>
    {
        IEnumerable<OrderConfirmation> GetOrderConfirmationWithDocumentByOrderId(int orderId);
    }
}
