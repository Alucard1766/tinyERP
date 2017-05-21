using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
