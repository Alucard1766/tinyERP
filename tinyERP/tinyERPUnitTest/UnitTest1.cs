using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyERP.Dal;
using tinyERP.Dal.Entities;

namespace tinyERPUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDBConnection()
        {
            using (var db = new TinyErpContext())
            {
                var budget = new Budget {Amount = 1000.0, Year = 2017};
                db.Budgets.Add(budget);
                db.SaveChanges();
            }
            Assert.IsTrue(true);
        }
    }
}