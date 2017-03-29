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
        public IBudgetRepository Budgets
        {
            get
            {
                return budgets ?? (budgets = new BudgetRepository(context));
            }
        }

        private ITransactionRepository transactions;
        public ITransactionRepository Transactions
        {
            get
            {
                return transactions ?? (transactions = new TransactionRepository(context));
            }
        }

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
