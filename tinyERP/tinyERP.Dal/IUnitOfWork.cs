using System;
using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork
    {
        IBudgetRepository Budgets { get; }
        ITransactionRepository Transactions { get; }
        ICategoryRepository Categories { get; }

        int Complete();
    }
}
