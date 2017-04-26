using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public TransactionRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetTransactionsBetween(DateTime from, DateTime to)
        {
            return (from t in TinyErpContext.Transactions
                    where @from <= t.Date && t.Date <= to
                    select t)
                    .ToList();
        }

        public IEnumerable<Transaction> GetTransactionsWithCategories()
        {
            return TinyErpContext.Transactions.Include(t => t.Category).ToList();
        }

        public IEnumerable<Transaction> GetTransactionsWithCategoriesBetween(DateTime from, DateTime to)
        {
            return (from t in TinyErpContext.Transactions
                    where @from <= to.Date && to.Date <= to
                    select t)
                    .Include(t => t.Category)
                    .ToList();
        }

        public IEnumerable<Transaction> GetTransactionsWithCategoriesFilteredBy(string searchTerm)
        {
            return (from t in TinyErpContext.Transactions
                    where t.Name.Contains(searchTerm) || t.Category.Name.Contains(searchTerm)
                    select t)
                    .Include(t => t.Category)
                    .ToList();
        }
    }
}
