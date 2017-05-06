using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public CategoryRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
