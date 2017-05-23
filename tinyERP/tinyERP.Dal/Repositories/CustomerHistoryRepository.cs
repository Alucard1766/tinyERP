using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CustomerHistoryRepository : Repository<CustomerHistory>, ICustomerHistoryRepository
    {
        public CustomerHistoryRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
