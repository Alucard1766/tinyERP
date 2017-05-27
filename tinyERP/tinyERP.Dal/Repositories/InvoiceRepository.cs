using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public InvoiceRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Invoice> GetInvoicesWithDocumentsByOrderId(int orderId)
        {
            return TinyErpContext.Invoices.Where(i => i.OrderId == orderId).Include(i => i.Document).ToList();
        }
    }
}
