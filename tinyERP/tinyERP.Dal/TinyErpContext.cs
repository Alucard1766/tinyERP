using System.Data.Entity;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Migrations;

namespace tinyERP.Dal
{
    public class TinyErpContext : DbContext
    {
        public TinyErpContext() : base("tinyERP")
        {
            Database.Initialize(false);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            //Real CodeFirst
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TinyErpContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasOptional(d => d.Offer).WithRequired(o => o.Document);
            modelBuilder.Entity<Document>().HasOptional(d => d.Invoice).WithRequired(i => i.Document);
            modelBuilder.Entity<Document>().HasOptional(d => d.OrderConfirmation).WithRequired(oc => oc.Document);
            //modelBuilder.Entity<Order>().HasOptional(o => o.OrderConfirmation).WithRequired(oc => oc.Order);
        }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerHistory> CustomerHistories { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
