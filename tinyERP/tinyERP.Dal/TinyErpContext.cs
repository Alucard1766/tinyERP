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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderConfirmation>().HasKey(oc => oc.OrderId);
            modelBuilder.Entity<OrderConfirmation>().HasRequired(oc => oc.Order).WithOptional(o => o.OrderConfirmation).WillCascadeOnDelete(true);
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

        public DbSet<OrderConfirmation> OrderConfirmations { get; set; }
    }
}
