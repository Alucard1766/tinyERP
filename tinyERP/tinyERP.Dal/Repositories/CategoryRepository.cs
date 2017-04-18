using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public CategoryRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategories()
        {
            return TinyErpContext.Categories.Include(c => c.Transactions).ToList();
        }
    }
}
