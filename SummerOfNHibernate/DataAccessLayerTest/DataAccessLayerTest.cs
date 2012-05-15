using System;
using System.Collections.Generic;
using System.Diagnostics;
using DataAccessLayer;
using DataTransfer;
using MbUnit.Framework;
using System.Linq;

namespace DataAccessLayerTest
{
    [TestFixture]
    public class DataAccessLayerTests
    {
        private NHibernateDataProvider provider;
        private const string customerFirstname = "Juan";
        private const string customerLastname = "Huerta";
        private const int numberOfOcurances = 1;

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

            var customers = provider.GetCustomerByFirstnameAndLastname(customerFirstname, customerLastname);

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
                Assert.GreaterThan(customer.Id, id);
            }


            Assert.GreaterThan(customers.Count, minimumNumerOfOccurances);
        }

        [Test]
        public void CreteriaAPI_CanGetCustomerByFirstName()
        {
            var customers = provider.CriteriaAPI_GetCustomerByFirstName("Juan");

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
            }

            Assert.AreEqual(customers.Count, numberOfOcurances);
        }

        [Test]
        public void CreteriaAPI_CanGetCustomersByFirsnameAndLastName()
        {
            var customers = provider.CriteriaAPI_GetCustomersByFirstNameAndLastName(customerFirstname, customerLastname);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
                Assert.AreEqual(customer.Lastname, customerLastname);
            }

            Assert.AreEqual(customers.Count, numberOfOcurances);
        }

        [Test]
        public void CreteriaAPI_CanGetCustomersGreaterThanId()
        {
            var id = 5;
            const int minimumNumerOfOccurances = 2;
            var customers = provider.CriteriaAPI_GetCustomersWithIdGreaterThan(id);

            foreach (var customer in customers)
            {
                Assert.GreaterThan(customer.Id, id);
            }

            Assert.GreaterThan(customers.Count, minimumNumerOfOccurances);
        }

        [Test]
        public void QueryByExample_CanGetCustomerByPassingACustomer()
        {
            const int minimumNumerOfOccurances = 1;

            const string lastName = "Fernandez";

            var customerSample = new Customer() {Lastname = lastName};

            var customers = provider.QueryByExample_GetCustomerByExample(customerSample);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Lastname, lastName);
            }


            Assert.GreaterThan(customers.Count, minimumNumerOfOccurances);
        }

        [Test]
        public void QueryByExample_CanGetCustomerByPassingACustomer_v2()
        {
            const string firstname = "Juan";
            const string lastname = "Huerta";

            var customerSample = new Customer() {Firstname = firstname, Lastname = lastname};

            var customers = provider.QueryByExample_GetCustomerByExample(customerSample);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, firstname);
                Assert.AreEqual(customer.Lastname, lastname);
            }
        }

        [Test]
        public void CanGetDistinctFirstNames()
        {
            IList<string> distinctFirstnames = provider.GetDistinctCustomerFirstNames();

            var minimumNumberOfDistinctNames = 10;

            var numberOfDisctinctStrings = distinctFirstnames.Distinct().Count();

            var numberOfTotalStrings = distinctFirstnames.Count();


            Assert.GreaterThan(numberOfTotalStrings, minimumNumberOfDistinctNames);
            Assert.AreEqual(numberOfDisctinctStrings, numberOfTotalStrings);
        }

        [Test]
        public void CriteriaAPI_CanGetDistinctFirstNames()
        {
            IList<string> distinctFirstnames = provider.CriteriaAPI_GetDistinctCustomerFirstNames();

            const int minimumNumberOfDistinctNames = 10;

            var numberOfDisctinctStrings = distinctFirstnames.Distinct().Count();

            var numberOfTotalStrings = distinctFirstnames.Count();

            Assert.GreaterThan(numberOfTotalStrings, minimumNumberOfDistinctNames);

            Assert.AreEqual(numberOfDisctinctStrings, numberOfTotalStrings);
        }

        [Test]
        public void CanRetrieveCustomersOrderBylastname()
        {
            IList<Customer> customers = provider.GetCustomersOrderByLastname();

            Customer priorCustomer = null;
            foreach (var customer in customers)
            {

                if (priorCustomer != null)
                {
                    Assert.GreaterThanOrEqualTo(customer.Lastname, priorCustomer.Lastname);
                }

                priorCustomer = customer;
            }

        }

        [Test]
        public void CriteriaAPI_CanRetrieveCustomersOrderBylastname()
        {
            IList<Customer> customers = provider.CriteriaAPI_GetCustomersOrderByLastname();

            Customer priorCustomer = null;
            foreach (var customer in customers)
            {
                if (priorCustomer != null)
                {
                    Assert.GreaterThanOrEqualTo(customer.Lastname, priorCustomer.Lastname);
                }

                priorCustomer = customer;
            }

        }

        [Test]
        public void CangetCountOfCustomerFirstname()
        {
            var expectedCounts = new Dictionary<string, int>
                                     {
                                         {"Juan", 1},
                                         {"Eduard", 6},
                                         {"Santiago", 2},
                                         {"Irene", 1},
                                         {"Aimee", 1},
                                         {"Angeli", 1},
                                         {"Poi", 1},
                                         {"Anshul", 1},
                                         {"Dvyia", 1},
                                         {"Hasmin", 1},
                                         {"Guillermo", 1},
                                         {"Jehanna", 1},
                                         {"Jon", 1},
                                         {"Kenny", 1},
                                         {"Lucas", 1},
                                         {"Maria", 1},
                                         {"Mariasun", 1},
                                         {"Piya", 1},
                                         {"Ram", 1},
                                         {"Unai", 1}
                                     };


            var firstNameCount = provider.GetCustomersFirstnameCount();

            foreach (var nameCount in firstNameCount)
            {
                var firstValue = expectedCounts[nameCount.Firstname];
                
                var secondValue = Convert.ToInt32(nameCount.Count);

                Assert.AreEqual(firstValue, secondValue);
            }
        }

    }
}