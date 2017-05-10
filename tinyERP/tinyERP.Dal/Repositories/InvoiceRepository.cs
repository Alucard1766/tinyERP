using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public InvoiceRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
