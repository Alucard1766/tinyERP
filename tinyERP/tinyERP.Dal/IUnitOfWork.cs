using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public interface IUnitOfWork
    {
        IBudgetRepository Budgets { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        ITransactionRepository Transactions { get; }
        ICustomerRepository Customers { get; }
        ICustomerHistoryRepository CustomerHistories { get; }
        IOfferRepository Offers { get; }
        IInvoiceRepository Invoices { get; }
        IOrderConfirmationRepository OrderConfirmations { get; }

        int Complete();
    }
}
