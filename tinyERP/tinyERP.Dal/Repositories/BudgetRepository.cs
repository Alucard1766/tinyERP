using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public BudgetRepository(TinyErpContext context) : base(context)
        {
        }

        public Budget GetBudgetByYear(DateTime date)
        {
            return (from t in TinyErpContext.Budgets
                    where t.Year == date.Year
                    select t)
                    .SingleOrDefault();
        }

        public IEnumerable<Budget> GetBudgetsWithTransactions()
        {
            return TinyErpContext.Budgets.Include(b => b.Transactions).ToList();
        }
    }
}
