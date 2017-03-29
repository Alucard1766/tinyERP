using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        private TinyErpContext tinyErpContext => context as TinyErpContext;

        public BudgetRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
