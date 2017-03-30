using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyERP.TestEnvrionment;

namespace tinyERP.Dal.Testing
{
    [TestClass]
    public class DalTest
    {
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void TestDbConnection()
        {
            Assert.IsTrue(true);
        }
    }
}