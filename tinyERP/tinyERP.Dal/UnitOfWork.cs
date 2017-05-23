using tinyERP.Dal.Repositories;

namespace tinyERP.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TinyErpContext context;

        private IBudgetRepository _budgets;
        private ICategoryRepository _categories;
        private IDocumentRepository _documents;
        private IOrderRepository _orders;
        private ITransactionRepository _transactions;
        private ICustomerRepository _customers;
        private ICustomerHistoryRepository _customerHistories;
        private IOfferRepository _offers;
        private IInvoiceRepository _invoices;
        private IOrderConfirmationRepository _orderConfirmations;

        public UnitOfWork(TinyErpContext context)
        {
            this.context = context;
        }

        public IBudgetRepository Budgets => _budgets ?? (_budgets = new BudgetRepository(context));

        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(context));

        public IDocumentRepository Documents => _documents ?? (_documents = new DocumentRepository(context));

        public IOrderRepository Orders => _orders ?? (_orders = new OrderRepository(context));

        public ITransactionRepository Transactions => _transactions ?? (_transactions = new TransactionRepository(context));

        public ICustomerRepository Customers => _customers ?? (_customers = new CustomerRepository(context));

        public ICustomerHistoryRepository CustomerHistories => _customerHistories ?? (_customerHistories = new CustomerHistoryRepository(context));

        public IOfferRepository Offers => _offers ?? (_offers = new OfferRepository(context));

        public IInvoiceRepository Invoices => _invoices ?? (_invoices = new InvoiceRepository(context));

        public IOrderConfirmationRepository OrderConfirmations => _orderConfirmations ?? (_orderConfirmations = new OrderConfirmationRepository(context));

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
