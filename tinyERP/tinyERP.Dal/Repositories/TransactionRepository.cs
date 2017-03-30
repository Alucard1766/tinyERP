using System;
using System.Collections.Generic;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private TinyErpContext tinyErpContext => context as TinyErpContext;

        public TransactionRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetTransactionsBetween(DateTime from, DateTime to)
        {
            return (from t in tinyErpContext.Transactions
                   where @from <= t.Date && t.Date <= to
                   select t).ToList();
        }
    }
}
