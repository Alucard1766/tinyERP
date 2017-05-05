using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public OrderRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetOrdersWithCustomers()
        {
            return TinyErpContext.Orders.Include(o => o.Customer).ToList();
        }
    }
}
