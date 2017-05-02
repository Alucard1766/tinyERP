using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context) : base(context)
        {
        }
    }
}
