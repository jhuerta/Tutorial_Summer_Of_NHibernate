using DataAccessLayer;
using MbUnit.Framework;


namespace DataAccessLayerTest
{
    [TestFixture]
    public class DataAccessLayerTests
    {
        private NHibernateDataProvider provider;
        const string customerFirstname = "Juan";
        const string customerLastname = "Huerta";
        const int numberOfOcurances = 1;

        [FixtureSetUp]
        public void FixtureSetup()
        {
            provider = new NHibernateDataProvider();
        }

        [Test]
        public void CanGetCustomerId()
        {
            var customerId = 1;

            var actual = provider.GetCustomerById(customerId);

            Assert.AreEqual(customerId, actual.Id);
   
        }

        [Test]
        public void ReturnsNullIfItdoesNotFindACustomer()
        {
            const int customerId = -1;

            var actual = provider.GetCustomerById(customerId);

            Assert.AreEqual(null, actual);

        }


        [Test]
        public void CanGetCustomersByFirsname()
        {

            var customers = provider.GetCustomersByFirstname("Juan");

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
            }

            Assert.AreEqual(customers.Count, numberOfOcurances);
        }

        [Test]
        public void CanGetCustomersByFirsnameWithParameters()
        {

            var customers = provider.GetCustomerByFirstnameWithParameters(customerFirstname);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
            }

            Assert.AreEqual(customers.Count, numberOfOcurances);

        }

        [Test]
        public void CanGetCustomersByFirsnameAndLastName()
        {

            var customers = provider.GetCustomerByFirstnameAndLastname(customerFirstname,customerLastname);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
                Assert.AreEqual(customer.Lastname, customerLastname);
            }
            
            Assert.AreEqual(customers.Count, numberOfOcurances);
        }

        [Test]
        public void CanGetCustomersGreaterThanId()
        {
            var id = 5;
            const int minimumNumerOfOccurances = 2;
            var customers = provider.GetCustomersWithIdGreaterThan(id);

            foreach (var customer in customers)
            {
                Assert.GreaterThan(customer.Id,id);
            }

            
            Assert.GreaterThan(customers.Count, minimumNumerOfOccurances);
        }

    }
}
