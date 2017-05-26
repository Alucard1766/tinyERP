using System;
using System.Collections.Generic;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Budget GetBudgetByYear(DateTime date);
        IEnumerable<Budget> GetBudgetsWithTransactions();
    }
}
