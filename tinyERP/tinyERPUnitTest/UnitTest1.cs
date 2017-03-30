using Microsoft.VisualStudio.TestTools.UnitTesting;
using tinyERP.Dal;
using tinyERP.Dal.Entities;
using tinyERP.TestEnvrionment;

namespace tinyERPUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void TestDBConnection()
        {
            Assert.IsTrue(true);
        }
    }
}