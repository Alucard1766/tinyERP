using System.Data.Entity;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Migrations;

namespace tinyERP.Dal
{
    public class TinyErpContext : DbContext
    {
        public TinyErpContext() : base("tinyERP")
        {
            Database.Initialize(true);
            Configuration.LazyLoadingEnabled = false;

            //Real CodeFirst
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TinyErpContext, Configuration>());
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}