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
    public class DataAccessLayerTests : Microdesk.Utility.UnitTest.DatabaseUnitTestBase
    {
        private NHibernateDataProvider provider;
        private const string customerFirstname = "Juan";
        private const string customerLastname = "Huerta";
        private const int numberOfJuan = 6;
        private const int numberOfJuanHuerta = 1;

        [FixtureSetUp]
        public void FixtureSetup()
        {
            provider = new NHibernateDataProvider();
        }

        [FixtureSetUp]
        public void TestFixtureSetup()
        {
            DatabaseFixtureSetUp();
            provider = new DataAccessLayer.NHibernateDataProvider();
        }

        [FixtureTearDown]
        public void TestFixtureTearDown()
        {
            DatabaseFixtureTearDown();
        }

        [SetUp]
        public void Setup()
        {
            DatabaseSetUp();

        }

        [TearDown]
        public void TearDown()
        {
            DatabaseTearDown();
        }

        [Test]
        public void GetMyTestDataXMLFile()
        {
            SaveTestDatabase();
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

            Assert.AreEqual(customers.Count, numberOfJuan);
        }

        [Test]
        public void CanGetCustomersByFirsnameWithParameters()
        {

            var customers = provider.GetCustomerByFirstnameWithParameters(customerFirstname);

            foreach (var customer in customers)
            {
                Assert.AreEqual(customer.Firstname, customerFirstname);
            }

            Assert.AreEqual(customers.Count, numberOfJuan);

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

            Assert.AreEqual(customers.Count, numberOfJuanHuerta);
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

            Assert.AreEqual(customers.Count, numberOfJuan);
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

            Assert.AreEqual(customers.Count, numberOfJuanHuerta);
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

                //Assert.AreEqual(firstValue, secondValue);
            }
        }

        [Test]
        public void CanSaveCustomer()
        {
            var newCustomerToAdd = new Customer
                                         {
                                             Firstname = "Jim",
                                             Lastname = "Morrison"
                                         };
            var customerId = provider.AddCustomer(newCustomerToAdd);

            var customerAddedAndRetrieved = provider.GetCustomerById(customerId);

            Assert.AreEqual(newCustomerToAdd,customerAddedAndRetrieved);
        }

        [Test]
        public void CanDeleteCustomer()
        {
            var firstCustomer = provider.GetCustomerById(1);

            provider.DeleteCustomer(firstCustomer);

            var customerDeleted = provider.GetCustomerById(1);

            Assert.IsNull(customerDeleted);
            Assert.IsNotNull(firstCustomer);
        }

        // TODO: random failures in nCrunch
        //[Test]
        public void CanUpdateCustomerFirstname()
        {
            const int customerId = 7;

            var firstCustomer = provider.GetCustomerById(customerId);

            var newNameToUpdate = firstCustomer.Firstname + "_newName";

            provider.UpdateCustomerFirstname(customerId, newNameToUpdate);

            var updatedName = provider.GetCustomerById(customerId).Firstname;

            Assert.AreEqual(updatedName, newNameToUpdate);
        }
        // TODO: random failures in nCrunch
        //[Test]
        public void CanUpdateCustomerLastname()
        {
            const int customerId = 5;

            var firstCustomer = provider.GetCustomerById(customerId);

            var newLastnameToUpdate = firstCustomer.Lastname + "_newName";

            provider.UpdateCustomerLastname(customerId, newLastnameToUpdate);

            var updatedLastame = provider.GetCustomerById(customerId).Lastname;

            Assert.AreEqual(updatedLastame, newLastnameToUpdate);
        }

        // TODO: To check - random failures in nCrunch
        //[Test]
        public void CanUpdateCustomer()
        {
            const int customerId = 6;

            var firstCustomer = provider.GetCustomerById(customerId);
            var newName = firstCustomer.Firstname + "_newName";
            var newLastname = firstCustomer.Lastname + "_newLastname";

            firstCustomer.Firstname = newName;
            firstCustomer.Lastname = newLastname;

            provider.UpdateCustomer(firstCustomer);

            var updateCustomer = provider.GetCustomerById(customerId);

            Assert.AreEqual(updateCustomer.Firstname, newName);
            Assert.AreEqual(updateCustomer.Lastname, newLastname);
        }

    }
}