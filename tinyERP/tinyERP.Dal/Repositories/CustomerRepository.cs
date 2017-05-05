using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public CustomerRepository(TinyErpContext context) : base(context)
        {
        }

    }
}
