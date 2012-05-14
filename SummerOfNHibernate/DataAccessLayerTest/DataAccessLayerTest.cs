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
            // Comment

            var provider = new NHibernateDataProvider();
            var actual = provider.GetCustomerById(1).Id;
            Assert.AreEqual(1, actual);

            // Comment

        }
    }

}
