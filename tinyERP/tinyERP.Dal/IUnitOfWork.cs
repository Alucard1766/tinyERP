using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork
    {
        IBudgetRepository Budgets { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        ITransactionRepository Transactions { get; }

        int Complete();
    }
}
