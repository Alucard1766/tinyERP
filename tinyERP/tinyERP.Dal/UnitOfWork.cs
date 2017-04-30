using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext context;

        private IBudgetRepository _budgets;
        private ICategoryRepository _categories;
        private IOrderRepository _orders;
        private ITransactionRepository _transactions;

        public UnitOfWork(TinyErpContext context)
        {
            this.context = context;
        }

        public IBudgetRepository Budgets => _budgets ?? (_budgets = new BudgetRepository(context));

        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(context));

        public IOrderRepository Orders => _orders ?? (_orders = new OrderRepository(context));

        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(context));

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
