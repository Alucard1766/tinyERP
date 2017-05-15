using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using tinyERP.Dal;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;

namespace tinyERP.TestEnvrionment
{
    public static class TestEnvironmentHelper
    {
        public static void InitializeTestData()
        {
            using (TinyErpContext context = new TinyErpContext())
            {
                var budget = context.Budgets.Add(new Budget { Year = 2017, Expenses = 1000.0, Revenue = 1200.0});
                var category = context.Categories.Add(new Category { Name = "Aufrag" });
                var transaction = context.Transactions.Add(
                    new Transaction
                    {
                        Name = "First Transaction 2017",
                        Amount = 200.0,
                        IsRevenue = true,
                        PrivatePart = 50,
                        Date = new DateTime(2017, 2, 3),
                        Comment = "Comment",
                        BudgetId = budget.Id,
                        CategoryId = category.Id
                    });
                var customer = context.Customers.Add(
                    new Customer
                    {
                        FirstName = "Max",
                        LastName = "Muster",
                        City = "Zürich",
                        Company = "HSR",
                        Email = "max.muster@hsr.ch",
                        Street = "Rappistr. 10",
                        Zip = 8000
                    });
                var customerHistory = context.CustomerHistories.Add(
                    new CustomerHistory
                    {
                        Text = "Kunde via Internet gefunden.",
                        Date = new DateTime(2017,2,3),
                        CustomerId = customer.Id

                    });
                var order = context.Orders.Add(new Order { Title = "Order", CustomerId = customer.Id});
                var document = context.Documents.Add(new Document
                {
                    Name = "Quittung 1",
                    RelativePath = "qu1.docx",
                    IssueDate = new DateTime(2017, 2, 2)
                });
                var offer = context.Offers.Add(new Offer
                {
                    OfferNumber = "ONr",
                    DocumentId = document.Id,
                    OrderId = order.Id
                });
                var invoice = context.Invoices.Add(new Invoice
                {
                    Amount = 100.0,
                    InvoiceNumber = "Invoice",
                    DocumentId = document.Id,
                    IsPayed = true,
                    OrderId = order.Id
                });

                context.SaveChanges();
            }

            using (TinyErpContext context = new TinyErpContext())
            {
                var budgetTableName = context.GetTableName<Budget>();
                var transactionTableName = context.GetTableName<Transaction>();
                var categoryTableName = context.GetTableName<Category>();
                var customerTableName = context.GetTableName<Customer>();
                var customerHistoryTableName = context.GetTableName<CustomerHistory>();
                var documentTableName = context.GetTableName<Document>();
                var orderTableName = context.GetTableName<Order>();
                var offerTableName = context.GetTableName<Offer>();
                var invoiceTableName = context.GetTableName<Invoice>();
                var orderConfirmationTableName = context.GetTableName<OrderConfirmation>();

                try
                {
                    context.DeleteAllRecords(offerTableName);
                    context.DeleteAllRecords(invoiceTableName);
                    context.DeleteAllRecords(orderConfirmationTableName);
                    context.DeleteAllRecords(budgetTableName);
                    context.DeleteAllRecords(transactionTableName);
                    context.DeleteAllRecords(categoryTableName);
                    context.DeleteAllRecords(orderTableName);
                    context.DeleteAllRecords(customerTableName);
                    context.DeleteAllRecords(customerHistoryTableName);
                    context.DeleteAllRecords(documentTableName);
                    
                    context.SetAutoIncrementOnTable(budgetTableName, true);
                    context.SetAutoIncrementOnTable(transactionTableName, true);
                    context.SetAutoIncrementOnTable(categoryTableName, true);
                    context.SetAutoIncrementOnTable(documentTableName, true);
                    context.SetAutoIncrementOnTable(customerTableName,true);
                    context.SetAutoIncrementOnTable(customerHistoryTableName, true);
                    context.SetAutoIncrementOnTable(orderTableName, true);
                    context.SetAutoIncrementOnTable(offerTableName, true);
                    context.SetAutoIncrementOnTable(invoiceTableName, true);
                    context.SetAutoIncrementOnTable(orderConfirmationTableName, true);

                    context.ResetEntitySeed(budgetTableName);
                    context.ResetEntitySeed(transactionTableName);
                    context.ResetEntitySeed(categoryTableName);
                    context.ResetEntitySeed(customerTableName);
                    context.ResetEntitySeed(customerHistoryTableName);
                    context.ResetEntitySeed(documentTableName);
                    context.ResetEntitySeed(orderTableName);
                    context.ResetEntitySeed(offerTableName);
                    context.ResetEntitySeed(invoiceTableName);
                    context.ResetEntitySeed(orderConfirmationTableName);

                    context.Budgets.AddRange(Budgets);
                    context.Categories.AddRange(Categories);
                    context.Transactions.AddRange(Transactions);
                    context.Documents.AddRange(Documents);
                    context.Customers.AddRange(Customers);
                    context.CustomerHistories.AddRange(CustomerHistories);
                    context.Orders.AddRange(Orders);
                    context.Offers.AddRange(Offers);
                    context.Invoices.AddRange(Invoices);
                    context.OrderConfirmations.AddRange(OrderConfirmations);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new ApplicationException("Error while reinit database", e);
                }
                finally
                {
                    context.SetAutoIncrementOnTable(budgetTableName, false);
                    context.SetAutoIncrementOnTable(transactionTableName, false);
                    context.SetAutoIncrementOnTable(categoryTableName, false);
                    context.SetAutoIncrementOnTable(customerTableName,false);
                    context.SetAutoIncrementOnTable(customerHistoryTableName,false);
                    context.SetAutoIncrementOnTable(documentTableName, false);
                    context.SetAutoIncrementOnTable(orderTableName, false);
                    context.SetAutoIncrementOnTable(offerTableName, false);
                    context.SetAutoIncrementOnTable(orderConfirmationTableName, false);
                }
            }
        }

        private static List<Budget> Budgets =>
           new List<Budget>
           {
                new Budget {Id = 1, Year = 2017, Expenses = 1000.0, Revenue = 1500.0},
                new Budget {Id = 2, Year = 2016, Expenses = 2000.0, Revenue = 2400.0},
                new Budget {Id = 3, Year = 2015, Expenses = 3000.0, Revenue = 3800.0},
           };

        private static List<Transaction> Transactions =>
           new List<Transaction>
           {
                new Transaction {Id = 1, Name = "First Transaction 2017", Amount = 200.0, IsRevenue = true, PrivatePart = 50, Date = new DateTime(2017, 2, 3), Comment = "Comment", BudgetId = 1, CategoryId = 1},
                new Transaction {Id = 2, Name = "Second Transaction 2017", Amount = 300.0, IsRevenue = false, PrivatePart = 60, Date = new DateTime(2017, 3, 3), BudgetId = 1, CategoryId = 2},
                new Transaction {Id = 3, Name = "First Transaction 2016", Amount = 400.0, IsRevenue = true, PrivatePart = 20, Date = new DateTime(2016, 3, 3), BudgetId = 2, CategoryId = 3},
                new Transaction {Id = 4, Name = "Second Transaction 2016", Amount = 10.0, IsRevenue = true, PrivatePart = 10, Date = new DateTime(2016, 7, 3), Comment = "...", BudgetId = 2, CategoryId = 2},
                new Transaction {Id = 5, Name = "Third Transaction 2016", Amount = 1337.40, IsRevenue = false, PrivatePart = 40, Date = new DateTime(2016, 8, 2), BudgetId = 2, CategoryId = 2}
           };

        private static List<Category> Categories =>
           new List<Category>
           {
                new Category {Id = 1, Name = "Büro", Comment = "Bleistifte!!"},
                new Category {Id = 2, Name = "Aufrag"},
                new Category {Id = 3, Name = "Auftragserstellung", ParentCategoryId = 2}
           };

        private static List<Customer> Customers =>
            new List<Customer>
            {
                new Customer{FirstName = "Max", LastName = "Muster", City = "Zürich", Company = "HSR", Email = "max.muster@hsr.ch", Street = "Rappistr. 10", Zip = 8000},
                new Customer{FirstName = "Peter", LastName = "Müller", City = "Uster", Street = "Usterstrasse 10", Zip = 5000},
                new Customer{FirstName = "Hans", LastName = "Keller", City = "Pfäffikon ZH", Email = "hans.keller@gmail.com", Street = "Pfäffikerstr. 20", Zip = 7000}
            };
        private static List<CustomerHistory> CustomerHistories =>
            new List<CustomerHistory>
            {
                new CustomerHistory{Text = "Kunde via Internet gefunden.", Date = new DateTime(2017,2,3), CustomerId = 1},
                new CustomerHistory{Text = "Kunde hat Rechnung zuspät bezahlt", Date = new DateTime(2017,3,3), CustomerId = 1},
                new CustomerHistory{Text = "Kunde wurde durch Max vermittelt", Date = new DateTime(2017,2,3), CustomerId = 2}
            };

        private static List<Document> Documents =>
            new List<Document>
            {
                new Document {Id = 1, Name = "OfferQuittung 1", RelativePath = "quittung1.docx", IssueDate = new DateTime(2017, 2, 2)},
                new Document {Id = 2, Name = "OfferQuittung 2", RelativePath = "quittung2.docx", IssueDate = new DateTime(2017, 2, 2)},
                new Document {Id = 3, Name = "InvoiceQuittung 3", RelativePath = "quittung3.docx", IssueDate = new DateTime(2017, 2, 2)}
            };

        private static List<Order> Orders =>
            new List<Order>
            {
                new Order {Id = 1, Title = "Auftrag 1", State = State.New, CreationDate = new DateTime(2017, 2, 3), StateModificationDate = new DateTime(2017, 2, 3), CustomerId = 2},
                new Order {Id = 2, Title = "Auftrag 2", State = State.InProgress, CreationDate = new DateTime(2017, 1, 23), StateModificationDate = new DateTime(2017, 2, 15), CustomerId = 1},
                new Order {Id = 3, Title = "Auftrag 3", State = State.Completed, CreationDate = new DateTime(2016, 7, 13), StateModificationDate = new DateTime(2016, 12, 20), CustomerId = 3},
            };

        private static List<Offer> Offers =>
            new List<Offer>
            {
                new Offer {Id = 1, OfferNumber = "Offer 1", OrderId = 2, DocumentId = 1},
                new Offer {Id = 2, OfferNumber = "Offer 2", OrderId = 3, DocumentId = 2},
                new Offer {Id = 3, OfferNumber = "Offer 3", OrderId = 2, DocumentId = 2}
            };

        private static List<Invoice> Invoices =>
            new List<Invoice>
            {
                new Invoice {Id = 1, InvoiceNumber= "Invoice 1", Amount = 150.0, IsPayed = true, OrderId = 2, DocumentId = 3}
            };

        private static List<OrderConfirmation> OrderConfirmations =>
            new List<OrderConfirmation>
            {
                new OrderConfirmation {OrderId = 1, DocumentId = 3, OrderConfNumber = "OrderConf"}
            };

        private static string GetTableName<T>(this DbContext context)
        {
            // See: https://lowrymedia.com/2014/06/10/ef6-1-mapping-between-types-tables-including-derived-types/
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);
            var entityTypeClr = metadata.GetItems<EntityType>(DataSpace.OSpace).Single(e => objectItemCollection.GetClrType(e) == typeof(T));
            var entityTypeCSpace = metadata.GetItems(DataSpace.CSpace).Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType).Cast<EntityType>().Single(x => x.Name == entityTypeClr.Name);
            var mappingsCSSpace = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace).Single().EntitySetMappings.ToList();

            EntitySet entitySet;
            var mapping = mappingsCSSpace.SingleOrDefault(x => x.EntitySet.Name == entityTypeCSpace.Name);
            if (mapping != null)
            {
                entitySet = mapping.EntityTypeMappings.Single().Fragments.Single().StoreEntitySet;
            }
            else
            {
                mapping = mappingsCSSpace.SingleOrDefault(x => x.EntityTypeMappings.Where(y => y.EntityType != null).Any(y => y.EntityType.Name == entityTypeCSpace.Name));
                if (mapping != null)
                {
                    entitySet = mapping.EntityTypeMappings.Where(x => x.EntityType != null).Single(x => x.EntityType.Name == entityTypeClr.Name).Fragments.Single().StoreEntitySet;
                }
                else
                {
                    var entitySetMapping = mappingsCSSpace.Single(x => x.EntityTypeMappings.Any(y => y.IsOfEntityTypes.Any(z => z.Name == entityTypeCSpace.Name)));
                    entitySet = entitySetMapping.EntityTypeMappings.First(x => x.IsOfEntityTypes.Any(y => y.Name == entityTypeCSpace.Name)).Fragments.Single().StoreEntitySet;
                }
            }

            string schema = (string)entitySet.MetadataProperties["Schema"].Value ?? entitySet.Schema;
            string table = (string)entitySet.MetadataProperties["Table"].Value ?? entitySet.Name;

            return $"[{schema}].[{table}]";
        }

        private static void DeleteAllRecords(this DbContext context, string table)
        {
            context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }

        private static void ResetEntitySeed(this DbContext context, string table)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{table}', RESEED, 0)");
            }
        }

        private static void SetAutoIncrementOnTable(this DbContext context, string table, bool isAutoIncrementOn)
        {
            if (context.TableHasIdentityColumn(table))
            {
                context.Database.ExecuteSqlCommand($"SET IDENTITY_INSERT {table} {(isAutoIncrementOn ? "ON" : "OFF")}");
            }
        }

        private static bool TableHasIdentityColumn(this DbContext context, string table)
        {
            return context.Database.SqlQuery<int>($"SELECT OBJECTPROPERTY(OBJECT_ID('{table}'), 'TableHasIdentity')").Single() == 1;
        }
    }
}
