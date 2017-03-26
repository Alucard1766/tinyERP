using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using tinyERP.Dal;
using tinyERP.Dal.Entities;

namespace tinyERP.BusinessLayer
{
    public class TinyErpBusinessComponent
    {
        public List<Budget> Budgets {
            get
            {
                using (var context = new TinyErpContext())
                {
                    return context.Budgets.ToList<Budget>();
                }
            }
        }

        public List<Transaction> Transactions
        {
            get
            {
                using (var context = new TinyErpContext())
                {
                    return context.Transactions.ToList<Transaction>();
                }
            }
        }

        public Budget GetBudgetById(int id)
        {
            using (var context = new TinyErpContext())
            {
                return context.Budgets.Find(id);
            }
        }

        public Transaction GetTransactionById(int id)
        {
            using (var context = new TinyErpContext())
            {
                return context.Transactions.Find(id);
            }
        }

        public Budget InsertBudget(Budget budget)
        {
            using (var context = new TinyErpContext())
            {
                context.Entry<Budget>(budget).State = EntityState.Added;
                context.SaveChanges();
                return budget;
            }
        }

        public Transaction InsertTransaction(Transaction transaction)
        {
            using (var context = new TinyErpContext())
            {
                context.Entry<Transaction>(transaction).State = EntityState.Added;
                context.SaveChanges();
                return transaction;
            }
        }

        public Budget UpdateBudget(Budget budget)
        {
            using (var context = new TinyErpContext())
            {
                try
                {
                    context.Entry<Budget>(budget).State = EntityState.Modified;
                    context.SaveChanges();
                    return budget;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, budget);
                }
            }
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            using (var context = new TinyErpContext())
            {
                try
                {
                    context.Entry<Transaction>(transaction).State = EntityState.Modified;
                    context.SaveChanges();
                    return transaction;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(context, transaction);
                }
            }
        }

        public void DeleteBudget(Budget budget)
        {
            using (var context = new TinyErpContext())
            {
                context.Entry<Budget>(budget).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteTransaction(Transaction transaction)
        {
            using (var context = new TinyErpContext())
            {
                context.Entry<Transaction>(transaction).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(TinyErpContext context, T entity) where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Error", dbEntity);
        }
    }
}
