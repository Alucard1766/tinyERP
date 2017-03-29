using System;
using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork : IDisposable
    {
        IBudgetRepository Budgets { get; }
        ITransactionRepository Transactions { get; }

        int Complete();
    }
}
