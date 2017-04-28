using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext context;

        private IBudgetRepository _budgets;
        private ICategoryRepository _categories;
        private IDocumentRepository _documents;
        private ITransactionRepository _transactions;

        public UnitOfWork(TinyErpContext context)
        {
            this.context = context;
        }

        public IBudgetRepository Budgets => _budgets ?? (_budgets = new BudgetRepository(context));

        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(context));

        public IDocumentRepository Documents => _documents ?? (_documents = new DocumentRepository(context));

        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(context));

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
