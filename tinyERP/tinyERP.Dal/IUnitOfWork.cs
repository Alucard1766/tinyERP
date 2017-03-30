using System;
using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork : IDisposable
    {
        IBudgetRepository Budgets { get; }
        ITransactionRepository Transactions { get; }
        ICategoryRepository Categories { get; }

        int Complete();
    }
}
