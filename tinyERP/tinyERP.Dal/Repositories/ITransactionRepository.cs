using System;
using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsBetween(DateTime from, DateTime to);
        IEnumerable<Transaction> GetTransactionsWithCategories();
        IEnumerable<Transaction> GetTransactionsWithCategoriesFilteredBy(string searchTerm);
        IEnumerable<Transaction> GetTransactionsWithCategoriesBetween(DateTime from, DateTime to);
    }
}
