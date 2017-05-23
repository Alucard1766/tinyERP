using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsWithCategories();
        IEnumerable<Transaction> GetTransactionsWithCategoriesFilteredBy(string searchTerm);
    }
}
