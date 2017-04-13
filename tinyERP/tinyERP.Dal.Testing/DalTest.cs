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
        private UnitOfWork _unitOfWork;

        [ClassCleanup]
        public static void CleanUpAfterAllTests()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            _unitOfWork = new UnitOfWork(new TinyErpContext());
        }

        [TestCleanup]
        public void CleanupTestData()
        {
            _unitOfWork?.Dispose();
        }

        [TestMethod]
        public void GetBudgetsTest()
        {
            var budgets = _unitOfWork.Budgets.GetAll();
            Assert.AreEqual(3, budgets.Count());
        }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = _unitOfWork.Categories.GetAll();
            Assert.AreEqual(3, categories.Count());
        }

        [TestMethod]
        public void GetTransactionsTest()
        {
            var transactions = _unitOfWork.Transactions.GetAll();
            Assert.AreEqual(5, transactions.Count());
        }

        [TestMethod]
        public void GetBudgetByIdTest()
        {
            var budget = _unitOfWork.Budgets.Get(1);
            Assert.AreEqual(2017, budget.Year);
        }

        [TestMethod]
        public void GetCategoryByIdTest()
        {
            var category = _unitOfWork.Categories.Get(1);
            Assert.AreEqual("Büro", category.Name);
        }

        [TestMethod]
        public void GetTransactionByIdTest()
        {
            var transaction = _unitOfWork.Transactions.Get(1);
            Assert.AreEqual(200, transaction.Amount);
        }

        [TestMethod]
        public void GetBudgetByNonexistingIdTest()
        {
            var budget = _unitOfWork.Budgets.Get(10);
            Assert.IsNull(budget);
        }

        [TestMethod]
        public void GetCategoryByNonexistingIdTest()
        {
            var category = _unitOfWork.Categories.Get(10);
            Assert.IsNull(category);
        }

        [TestMethod]
        public void GetTransactionByNonexistingIdTest()
        {
            var transaction = _unitOfWork.Transactions.Get(10);
            Assert.IsNull(transaction);
        }

        [TestMethod]
        public void InsertBudgetTest()
        {
            var budget = new Budget {Expenses = 1337.0, Year = 2042, Revenue = 1400.0};
            var returned = _unitOfWork.Budgets.Add(budget);
            _unitOfWork.Complete();
            Assert.AreEqual(budget.Expenses, returned.Expenses);
        }

        [TestMethod]
        public void InsertCategoryTest()
        {
            var category = new Category { Name = "Test" };
            var returned = _unitOfWork.Categories.Add(category);
            _unitOfWork.Complete();
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
            var returned = _unitOfWork.Transactions.Add(transaction);
            _unitOfWork.Complete();
            Assert.AreEqual(transaction.Name, returned.Name);
        }

        [TestMethod]
        public void UpdateBudgetTest()
        {
            var budget = _unitOfWork.Budgets.Get(1);
            budget.Expenses = 1500.0;
            _unitOfWork.Complete();
            var changed = _unitOfWork.Budgets.Get(1);
            Assert.AreEqual(1500.0, changed.Expenses);
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var category = _unitOfWork.Categories.Get(1);
            category.Name = "ChangedName";
            _unitOfWork.Complete();
            var changed = _unitOfWork.Categories.Get(1);
            Assert.AreEqual("ChangedName", changed.Name);
        }

        [TestMethod]
        public void UpdateTransactionTest()
        {
            var transaction = _unitOfWork.Transactions.Get(1);
            transaction.Amount = 42.0;
            _unitOfWork.Complete();
            var changed = _unitOfWork.Transactions.Get(1);
            Assert.AreEqual(42.0, changed.Amount);
        }

        [TestMethod]
        public void DeleteBudgetTest()
        {
            _unitOfWork.Budgets.Remove(_unitOfWork.Budgets.Get(1));
            _unitOfWork.Complete();
            Assert.AreEqual(2, _unitOfWork.Budgets.GetAll().Count());
        }

        [TestMethod]
        public void DeleteCategoryTest()
        {
            _unitOfWork.Categories.Remove(_unitOfWork.Categories.Get(1));
            _unitOfWork.Complete();
            Assert.AreEqual(2, _unitOfWork.Categories.GetAll().Count());
        }

        [TestMethod]
        public void DeleteTransactionTest()
        {
            _unitOfWork.Transactions.Remove(_unitOfWork.Transactions.Get(1));
            _unitOfWork.Complete();
            Assert.AreEqual(4, _unitOfWork.Transactions.GetAll().Count());
        }

        [TestMethod]
        public void GetBudgetByYearTest()
        {
            var budget = _unitOfWork.Budgets.GetBudgetByYear(new DateTime(2017, 04, 01));
            Assert.AreEqual(1, budget.Id);
        }
    }
}
