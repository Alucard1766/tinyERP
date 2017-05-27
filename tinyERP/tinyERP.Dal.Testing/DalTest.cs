using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyERP.Dal.Entities;
using tinyERP.Dal.Types;
using tinyERP.TestEnvrionment;

namespace tinyERP.Dal.Testing
{
    [TestClass]
    public class DalTest
    {
        private TinyErpContext context;
        private UnitOfWork unitOfWork;
        private const string FileToAdd = "filetoadd.txt";
        private const string FileToDelete = "filetodelete.txt";

        [ClassInitialize]
        public static void InitializeBeforeAllTests(TestContext context)
        {
            File.Create(FileToAdd);
            Directory.CreateDirectory(FileType.Document.ToString());
            File.Create(Path.Combine(FileType.Document.ToString(), FileToDelete));
        }

        [ClassCleanup]
        public static void CleanUpAfterAllTests()
        {
            TestEnvironmentHelper.InitializeTestData();
            File.Delete(FileToAdd);
            File.Delete(FileToDelete);
            File.Delete(Path.Combine(FileType.Document.ToString(), FileToAdd));
            File.Delete(Path.Combine(FileType.Document.ToString(), FileToDelete));
            Directory.Delete(FileType.Document.ToString());
        }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
            context = new TinyErpContext();
            unitOfWork = new UnitOfWork(context);
        }

        #region Budget-Tests

        [TestMethod]
        public void GetBudgetsTest()
        {
            var budgets = unitOfWork.Budgets.GetAll();
            Assert.AreEqual(3, budgets.Count());
        }

        [TestMethod]
        public void InsertBudgetTest()
        {
            var budget = unitOfWork.Budgets.Add(new Budget
            {
                Expenses = 1337.0,
                Year = 2042,
                Revenue = 1400.0
            });
            unitOfWork.Complete();
            Assert.AreEqual(budget, context.Budgets.OrderByDescending(b => b.Id).First());
        }

        [TestMethod]
        public void DeleteBudgetTest()
        {
            unitOfWork.Budgets.Remove(context.Budgets.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Budgets.Count());
        }

        [TestMethod]
        public void GetBudgetByYearTest()
        {
            var budget = unitOfWork.Budgets.GetBudgetByYear(new DateTime(2017, 04, 01));
            Assert.AreEqual(1, budget.Id);
        }

        #endregion

        #region Category-Tests

        [TestMethod]
        public void GetCategoriesTest()
        {
            var categories = unitOfWork.Categories.GetAll();
            Assert.AreEqual(3, categories.Count());
        }

        [TestMethod]
        public void InsertCategoryTest()
        {
            var category = unitOfWork.Categories.Add(new Category
            {
                Name = "Test"
            });
            unitOfWork.Complete();
            Assert.AreEqual(category, context.Categories.OrderByDescending(c => c.Id).First());
        }


        [TestMethod]
        public void DeleteCategoryTest()
        {
            unitOfWork.Categories.Remove(context.Categories.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Categories.Count());
        }

        #endregion

        #region Customer-Tests

        [TestMethod]
        public void GetCustomersTest()
        {
            var customers = unitOfWork.Customers.GetAll();
            Assert.AreEqual(3, customers.Count());
        }

        [TestMethod]
        public void InsertCustomerTest()
        {
            var customer = unitOfWork.Customers.Add(new Customer
            {
                FirstName = "Sepp",
                LastName = "Hauser",
                City = "Zürich",
                Company = "Buurehof",
                Email = "sepp.hauser@buure.ch",
                Street = "Feldstr. 10",
                Zip = 2000
            });
            unitOfWork.Complete();
            Assert.AreEqual(customer, context.Customers.OrderByDescending(c => c.Id).First());
        }

        [TestMethod]
        public void DeleteCustomerTest()
        {
            unitOfWork.Customers.Remove(context.Customers.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Customers.Count());
        }

        #endregion

        #region CustomerHistory-Tests

        [TestMethod]
        public void GetCustomerHistoriesTest()
        {
            var customerHistories = unitOfWork.CustomerHistories.GetAll();
            Assert.AreEqual(3, customerHistories.Count());
        }

        [TestMethod]
        public void InsertCustomerHistoryTest()
        {
            var history = unitOfWork.CustomerHistories.Add(new CustomerHistory
            {
                Text = "Welcome Back",
                Date = new DateTime(2017, 04, 27),
                Customer = context.Customers.First()
            });
            unitOfWork.Complete();
            Assert.AreEqual(history, context.CustomerHistories.OrderByDescending(c => c.Id).First());
        }

        [TestMethod]
        public void DeleteCustomerHistoryTest()
        {
            unitOfWork.CustomerHistories.Remove(context.CustomerHistories.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.CustomerHistories.Count());
        }

        #endregion

        #region Document-Tests

        [TestMethod]
        public void GetDocumentsTest()
        {
            var documents = unitOfWork.Documents.GetAll();
            Assert.AreEqual(3, documents.Count());
        }

        [TestMethod]
        public void InsertDocumentTest()
        {
            var document = unitOfWork.Documents.Add(new Document
            {
                Name = "TestDoc",
                RelativePath = "cake",
                IssueDate = new DateTime(2017, 4, 2)
            });
            unitOfWork.Complete();
            Assert.AreEqual(document, context.Documents.OrderByDescending(d => d.Id).First());
        }

        [TestMethod]
        public void DeleteDocumentTest()
        {
            unitOfWork.Documents.Remove(context.Documents.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Documents.Count());
        }

        #endregion

        #region Invoice-Tests

        [TestMethod]
        public void GetInvoicesTest()
        {
            var invoices = unitOfWork.Invoices.GetAll();
            Assert.AreEqual(3, invoices.Count());
        }

        [TestMethod]
        public void InsertInvoiceTest()
        {
            var invoice = unitOfWork.Invoices.Add(new Invoice
            {
                Amount = 15,
                InvoiceNumber = "InvX",
                IsPayed = false,
                Order = context.Orders.First(),
                Document = context.Documents.First()
            });
            unitOfWork.Complete();
            Assert.AreEqual(invoice, context.Invoices.OrderByDescending(i => i.Id).First());
        }

        [TestMethod]
        public void DeleteInvoiceTest()
        {
            unitOfWork.Invoices.Remove(context.Invoices.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Invoices.Count());
        }

        [TestMethod]
        public void GetInvoicesWithDocumentsByOrderIdTest()
        {
            var invoices = unitOfWork.Invoices.GetInvoicesWithDocumentsByOrderId(context.Orders.First().Id);
            Assert.AreEqual(2, invoices.Count(i => i.Document != null));
        }

        #endregion

        #region Offer-Tests

        [TestMethod]
        public void GetOffersTest()
        {
            var offers = unitOfWork.Offers.GetAll();
            Assert.AreEqual(3, offers.Count());
        }

        [TestMethod]
        public void InsertOfferTest()
        {
            var offer = unitOfWork.Offers.Add(new Offer
            {
                OfferNumber = "OffX",
                Order = context.Orders.First(),
                Document = context.Documents.First()
            });
            unitOfWork.Complete();
            Assert.AreEqual(offer, context.Offers.OrderByDescending(o => o.Id).First());
        }

        [TestMethod]
        public void DeleteOfferTest()
        {
            unitOfWork.Offers.Remove(context.Offers.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Offers.Count());
        }

        [TestMethod]
        public void GetOffersWithDocumentsByOrderIdTest()
        {
            var offers = unitOfWork.Offers.GetOffersWithDocumentsByOrderId(context.Orders.First().Id);
            Assert.AreEqual(1, offers.Count(o => o.Document != null));
        }

        #endregion

        #region Order-Tests

        [TestMethod]
        public void GetOrdersTest()
        {
            var orders = unitOfWork.Orders.GetAll();
            Assert.AreEqual(3, orders.Count());
        }

        [TestMethod]
        public void InsertOrderTest()
        {
            var order = unitOfWork.Orders.Add(new Order
            {
                Title = "Order",
                State = State.InProgress,
                CreationDate = DateTime.Today,
                StateModificationDate = DateTime.Today,
                CustomerId = 1
            });
            unitOfWork.Complete();
            Assert.AreEqual(order, context.Orders.OrderByDescending(o => o.Id).First());
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            unitOfWork.Orders.Remove(context.Orders.First());
            unitOfWork.Complete();
            Assert.AreEqual(2, context.Orders.Count());
        }

        [TestMethod]
        public void GetOrdersWithCustomersTest()
        {
            var orders = unitOfWork.Orders.GetOrdersWithCustomers();
            Assert.AreEqual(3, orders.Count(o => o.Customer != null));
        }

        #endregion

        #region OrderConfirmation-Tests

        [TestMethod]
        public void GetOrderConfirmationsTest()
        {
            var orderConfirmations = unitOfWork.OrderConfirmations.GetAll();
            Assert.AreEqual(1, orderConfirmations.Count());
        }

        [TestMethod]
        public void InsertOrderConfirmationTest()
        {
            var order = context.Orders.First(o => o.OrderConfirmation == null);
            var orderConfirmation = unitOfWork.OrderConfirmations.Add(new OrderConfirmation
            {
                OrderConfNumber = "OrConfX",
                Order = order,
                Document = context.Documents.First()
            });
            unitOfWork.Complete();
            Assert.AreEqual(orderConfirmation, context.OrderConfirmations.First(o => o.OrderId == order.Id));
        }

        [TestMethod]
        public void DeleteOrderConfirmationTest()
        {
            unitOfWork.OrderConfirmations.Remove(context.OrderConfirmations.First());
            unitOfWork.Complete();
            Assert.AreEqual(0, context.OrderConfirmations.Count());
        }

        [TestMethod]
        public void GetOrderConfirmationWithDocumentByOrderIdTest()
        {
            var orderConfirmation = unitOfWork.OrderConfirmations.GetOrderConfirmationWithDocumentByOrderId(context.Orders.First().Id);
            Assert.AreEqual(1, orderConfirmation.Count(o => o.Document != null));
        }

        #endregion

        #region Transaction-Tests

        [TestMethod]
        public void GetTransactionsTest()
        {
            var transactions = unitOfWork.Transactions.GetAll();
            Assert.AreEqual(5, transactions.Count());
        }

        [TestMethod]
        public void InsertTransactionTest()
        {
            var transaction = unitOfWork.Transactions.Add(new Transaction
            {
                Amount = 10.0,
                BudgetId = 1,
                CategoryId = 1,
                Name = "TestTransacton",
                PrivatePart = 10,
                Date = DateTime.Now
            });
            unitOfWork.Complete();
            Assert.AreEqual(transaction, context.Transactions.OrderByDescending(t => t.Id).First());
        }

        [TestMethod]
        public void DeleteTransactionTest()
        {
            unitOfWork.Transactions.Remove(context.Transactions.First());
            unitOfWork.Complete();
            Assert.AreEqual(4, context.Transactions.Count());
        }

        [TestMethod]
        public void GetTransactionsWithCategoriesTest()
        {
            var transactions = unitOfWork.Transactions.GetTransactionsWithCategories();
            Assert.AreEqual(5, transactions.Count(t => t.Category != null));
        }

        [TestMethod]
        public void GetTransactionsWithCategoriesFilteredByTest()
        {
            var transactions = unitOfWork.Transactions.GetTransactionsWithCategoriesFilteredBy("Second");
            Assert.AreEqual(2, transactions.Count(t => t.Category != null));
        }

        #endregion
        
        #region FileAccess-Tests

        [TestMethod]
        public void AddFileTest()
        {
            var fileName = FileAccess.Add(FileToAdd, FileType.Document);
            Assert.IsTrue(File.Exists(Path.Combine(FileType.Document.ToString(), fileName)));
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var file = Path.Combine(FileType.Document.ToString(), FileToDelete);
            Assert.IsTrue(File.Exists(file));
            FileAccess.Delete(FileToDelete);
            Assert.IsFalse(File.Exists(file));
        }
        
        #endregion
    }
}
