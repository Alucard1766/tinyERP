using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyERP.Dal.Entities;
using tinyERP.TestEnvrionment;

namespace tinyERP.Dal.Testing
{
    [TestClass]
    public class DalTest
    {
        private UnitOfWork unitOfWork;

        [ClassCleanup]
        public static void CleanUpAfterAllTests()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            unitOfWork = new UnitOfWork(new TinyErpContext());
        }

        [TestMethod]
        public void GetBudgetsTest()
        {
            var budgets = unitOfWork.Budgets.GetAll();
            Assert.AreEqual(3, budgets.Count());
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = unitOfWork.Categories.GetAll();
            Assert.AreEqual(3, categories.Count());
        }

        [TestMethod]
        public void GetTransactionsTest()
        {
            var transactions = unitOfWork.Transactions.GetAll();
            Assert.AreEqual(5, transactions.Count());
        }

        [TestMethod]
        public void GetBudgetByIdTest()
        {
            var budget = unitOfWork.Budgets.Get(1);
            Assert.AreEqual(2017, budget.Year);
        }

        [TestMethod]
        public void GetCategoryByIdTest()
        {
            var category = unitOfWork.Categories.Get(1);
            Assert.AreEqual("Büro", category.Name);
        }

        [TestMethod]
        public void GetTransactionByIdTest()
        {
            var transaction = unitOfWork.Transactions.Get(1);
            Assert.AreEqual(200, transaction.Amount);
        }

        [TestMethod]
        public void GetBudgetByNonexistingIdTest()
        {
            var budget = unitOfWork.Budgets.Get(10);
            Assert.IsNull(budget);
        }

        [TestMethod]
        public void GetCategoryByNonexistingIdTest()
        {
            var category = unitOfWork.Categories.Get(10);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetTransactionByNonexistingIdTest()
        {
            var transaction = unitOfWork.Transactions.Get(10);
            Assert.IsNull(transaction);
        }

        [TestMethod]
        public void InsertBudgetTest()
        {
            var budget = new Budget {Expenses = 1337.0, Year = 2042, Revenue = 1400.0};
            var returned = unitOfWork.Budgets.Add(budget);
            unitOfWork.Complete();
            Assert.AreEqual(budget.Expenses, returned.Expenses);
        }

        [TestMethod]
        public void InsertCategoryTest()
        {
            var category = new Category { Name = "Test" };
            var returned = unitOfWork.Categories.Add(category);
            unitOfWork.Complete();
            Assert.AreEqual(category.Name, returned.Name);
        }

        [TestMethod]
        public void InsertTransactionTest()
        {
            var transaction = new Transaction
            {
                Amount = 10.0,
                BudgetId = 1,
                CategoryId = 1,
                Name = "TestTransacton",
                PrivatePart = 10,
                Date = DateTime.Now
            };
            var returned = unitOfWork.Transactions.Add(transaction);
            unitOfWork.Complete();
            Assert.AreEqual(transaction.Name, returned.Name);
        }

        [TestMethod]
        public void UpdateBudgetTest()
        {
            var budget = unitOfWork.Budgets.Get(1);
            budget.Expenses = 1500.0;
            unitOfWork.Complete();
            var changed = unitOfWork.Budgets.Get(1);
            Assert.AreEqual(1500.0, changed.Expenses);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = unitOfWork.Categories.Get(1);
            category.Name = "ChangedName";
            unitOfWork.Complete();
            var changed = unitOfWork.Categories.Get(1);
            Assert.AreEqual("ChangedName", changed.Name);
        }

        [TestMethod]
        public void UpdateTransactionTest()
        {
            var transaction = unitOfWork.Transactions.Get(1);
            transaction.Amount = 42.0;
            unitOfWork.Complete();
            var changed = unitOfWork.Transactions.Get(1);
            Assert.AreEqual(42.0, changed.Amount);
        }

        [TestMethod]
        public void DeleteBudgetTest()
        {
            unitOfWork.Budgets.Remove(unitOfWork.Budgets.Get(1));
            unitOfWork.Complete();
            Assert.AreEqual(2, unitOfWork.Budgets.GetAll().Count());
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            unitOfWork.Categories.Remove(unitOfWork.Categories.Get(1));
            unitOfWork.Complete();
            Assert.AreEqual(2, unitOfWork.Categories.GetAll().Count());
        }

        [TestMethod]
        public void DeleteTransactionTest()
        {
            unitOfWork.Transactions.Remove(unitOfWork.Transactions.Get(1));
            unitOfWork.Complete();
            Assert.AreEqual(4, unitOfWork.Transactions.GetAll().Count());
        }

        [TestMethod]
        public void GetBudgetByYearTest()
        {
            var budget = unitOfWork.Budgets.GetBudgetByYear(new DateTime(2017, 04, 01));
            Assert.AreEqual(1, budget.Id);
        }
    }
}
