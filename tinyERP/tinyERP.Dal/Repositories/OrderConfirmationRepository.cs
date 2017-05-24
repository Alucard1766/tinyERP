using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OrderConfirmationRepository : Repository<OrderConfirmation>, IOrderConfirmationRepository
    {
        public OrderConfirmationRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
