using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
