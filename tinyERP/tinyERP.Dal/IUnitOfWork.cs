using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork
    {
        IBudgetRepository Budgets { get; }
        ITransactionRepository Transactions { get; }
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        ICustomerHistoryRepository CustomerHistories { get; }
        int Complete();
    }
}
