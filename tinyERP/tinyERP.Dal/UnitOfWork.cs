using System;
using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext context;

        public UnitOfWork(TinyErpContext context)
        {
            this.context = context;
        }

        private IBudgetRepository budgets;
        public IBudgetRepository Budgets => budgets ?? (budgets = new BudgetRepository(context));

        private ITransactionRepository transactions;
        public ITransactionRepository Transactions => transactions ?? (transactions = new TransactionRepository(context));

        private ICategoryRepository categories;

        public ICategoryRepository Categories => categories ?? (categories = new CategoryRepository(context));

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
