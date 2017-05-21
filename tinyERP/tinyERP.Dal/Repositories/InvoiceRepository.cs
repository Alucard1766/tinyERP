using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
