using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CustomerHistoryRepository : Repository<CustomerHistory>, ICustomerHistoryRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public CustomerHistoryRepository(TinyErpContext context) : base(context)
        {
        }

    }
}
