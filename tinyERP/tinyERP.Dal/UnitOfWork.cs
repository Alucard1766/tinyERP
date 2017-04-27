using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext context;

        private IBudgetRepository _budgets;
        private ICategoryRepository _categories;
        private ITransactionRepository _transactions;
        private ICustomerRepository _customers;
        private ICustomerHistoryRepository _customerHistories;

        public UnitOfWork(TinyErpContext context)
        {
            this.context = context;
        }

        public IBudgetRepository Budgets => _budgets ?? (_budgets = new BudgetRepository(context));

        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(context));

        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(context));

        public ICustomerRepository Customers => _customers ?? (_customers = new CustomerRepository(context));
        public ICustomerHistoryRepository CustomerHistories => _customerHistories ?? (_customerHistories = new CustomerHistoryRepository(context));
        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
