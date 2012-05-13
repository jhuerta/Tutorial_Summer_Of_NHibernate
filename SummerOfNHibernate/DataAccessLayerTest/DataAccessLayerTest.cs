using DataAccessLayer;
using MbUnit.Framework;


namespace DataAccessLayerTest
{
    [TestFixture]
    public class DataAccessLayerTests
    {



        [Test]
        public void CanGetCustomerId()
        {
            var provider = new NHibernateDataProvider();
            var actual = provider.GetCustomerById(1).Id;
            Assert.AreEqual(1, actual);

        }
    }

}
