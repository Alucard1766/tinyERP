using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private TinyErpContext tinyErpContext => context as TinyErpContext;

        public TransactionRepository(TinyErpContext context) : base(context)
        {
        }
    }
}
