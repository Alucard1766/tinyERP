using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OrderConfirmationRepository : Repository<OrderConfirmation>, IOrderConfirmationRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public OrderConfirmationRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<OrderConfirmation> GetOrderConfirmationWithDocumentByOrderId(int orderId)
        {
            return TinyErpContext.OrderConfirmations.Where(o => o.OrderId == orderId).Include(o => o.Document).ToList();
        }
    }
}
