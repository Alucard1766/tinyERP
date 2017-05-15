using System.Data.Entity;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Migrations;

namespace tinyERP.Dal
{
    public class TinyErpContext : DbContext
    {
        public TinyErpContext() : base("tinyERP")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //Real CodeFirst
            Database.SetInitializer(new TinyErpDBInitializer());
        }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerHistory> CustomerHistories { get; set; }
    }
}
