using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using tinyERP.Dal;
using tinyERP.Dal.Entities;

namespace tinyERP.TestEnvrionment
{
    public static class TestEnvironmentHelper
    {
        public static void InitializeTestData()
        {
            using (TinyErpContext context = new TinyErpContext())
            {
                var budget = context.Budgets.Add(new Budget { Year = 2017, Amount = 1000.0 });
                var category = context.Categories.Add(new Category { Name = "Aufrag" });
                var transaction = context.Transactions.Add(
                    new Transaction
                    {
                        Name = "First Transaction 2017",
                        Amount = 200.0,
                        IsEarning = true,
                        PrivatePart = 50,
                        Date = new DateTime(2017, 2, 3),
                        Comment = "Comment",
                        BudgetId = budget.Id,
                        CategoryId = category.Id
                    });

                context.SaveChanges();
            }

            using (TinyErpContext context = new TinyErpContext())
            {
                var budgetTableName = context.GetTableName<Budget>();
                var transactionTableName = context.GetTableName<Transaction>();
                var categoryTableName = context.GetTableName<Category>();

                try
                {
                    context.DeleteAllRecords(budgetTableName);
                    context.DeleteAllRecords(transactionTableName);
                    context.DeleteAllRecords(categoryTableName);

                    context.SetAutoIncrementOnTable(budgetTableName, true);
                    context.SetAutoIncrementOnTable(transactionTableName, true);
                    context.SetAutoIncrementOnTable(categoryTableName, true);

                    context.ResetEntitySeed(budgetTableName);
                    context.ResetEntitySeed(transactionTableName);
                    context.ResetEntitySeed(categoryTableName);

                    context.Budgets.AddRange(Budgets);
                    context.Categories.AddRange(Categories);
                    context.Transactions.AddRange(Transactions);
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
                }

            }
        }

        private static List<Budget> Budgets =>
           new List<Budget>
           {
                new Budget {Id = 1, Year = 2017, Amount = 1000.0},
                new Budget {Id = 2, Year = 2016, Amount = 2000.0},
                new Budget {Id = 3, Year = 2015, Amount = 3000.0},
           };

        private static List<Transaction> Transactions =>
           new List<Transaction>
           {
                new Transaction {Id = 1, Name = "First Transaction 2017", Amount = 200.0, IsEarning = true, PrivatePart = 50, Date = new DateTime(2017, 2, 3), Comment = "Comment", BudgetId = 1, CategoryId = 1},
                new Transaction {Id = 2, Name = "Second Transaction 2017", Amount = 300.0, IsEarning = false, PrivatePart = 60, Date = new DateTime(2017, 3, 3), BudgetId = 1, CategoryId = 2},
                new Transaction {Id = 3, Name = "First Transaction 2016", Amount = 400.0, IsEarning = true, PrivatePart = 20, Date = new DateTime(2016, 3, 3), BudgetId = 2, CategoryId = 3},
                new Transaction {Id = 4, Name = "Second Transaction 2016", Amount = 10.0, IsEarning = true, PrivatePart = 10, Date = new DateTime(2016, 7, 3), Comment = "...", BudgetId = 2, CategoryId = 2},
                new Transaction {Id = 5, Name = "Third Transaction 2016", Amount = 1337.40, IsEarning = false, PrivatePart = 40, Date = new DateTime(2016, 8, 2), BudgetId = 2, CategoryId = 2}
           };

        private static List<Category> Categories =>
           new List<Category>
           {
                new Category {Id = 1, Name = "Büro", Comment = "Bleistifte!!"},
                new Category {Id = 2, Name = "Aufrag"},
                new Category {Id = 3, Name = "Auftragserstellung", ParentCategoryId = 2}
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
