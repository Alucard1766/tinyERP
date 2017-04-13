using System.Data.Entity;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Migrations;

namespace tinyERP.Dal
{
    public class TinyErpContext : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public TinyErpContext() : base("tinyERP")
        {
            Database.Initialize(false);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //Real CodeFirst
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TinyErpContext, Configuration>());
        }
    }
}
