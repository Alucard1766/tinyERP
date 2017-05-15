using System;
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
    }
}
