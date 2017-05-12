using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrdersWithCustomers();
        IEnumerable<Offer> GetOffersAndDocumentsByOrderId(int orderId);
    }
}
