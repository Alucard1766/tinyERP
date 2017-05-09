using System.Data.Entity;
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
