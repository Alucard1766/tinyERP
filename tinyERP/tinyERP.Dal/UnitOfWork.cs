using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext _context;

        private IBudgetRepository _budgets;
        public IBudgetRepository Budgets => _budgets ?? (_budgets = new BudgetRepository(_context));

        private ITransactionRepository _transactions;
        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(_context));

        private ICategoryRepository _categories;
        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(_context));

        public UnitOfWork(TinyErpContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
