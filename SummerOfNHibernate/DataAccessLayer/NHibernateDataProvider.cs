using System;
using System.Collections.Generic;
using DataTransfer;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace DataAccessLayer
{
    public class NHibernateDataProvider
    {
        private readonly ISession _session;

        public NHibernateDataProvider()
        {
            _session = GetSession();
        }

        public Customer GetCustomerById(int customerId)
        {   
            return _session.Get<Customer>(customerId);
        }

        private static ISession GetSession()
        {
            var configuration = new Configuration();

            configuration.Configure("hibernate.cfg.xml");

            var sessionFactory = configuration.BuildSessionFactory();

            var session = sessionFactory.OpenSession();
            return session;
        }

        /*public Customer GetCustomerById(int customerId)
        {
             1. Get config file
             2. Factory will build session with the config file
             3. Both factory and config file are disposable
             Therefore: We can combine and not stored variables
            var config = new Configuration();
            config.Configure();
            var sessionFactory = config.BuildSessionFactory();
            var session = sessionFactory.OpenSession();


             This would work but the risk is that it will try to cast the return into an object
             This cast operation may fail.
            return (DataTransfer.Customer) session.Get(typeof (DataTransfer.Customer), customerId);

             Because the cast may fail, we'd rather go for  generics.
             return session.Get<Customer>(customerId);
        }*/

        public IList<Customer> GetCustomersByFirstname(string firstname)
        {

            var queryString = string.Format("select c from Customer c where c.Firstname = '{0}'", firstname);

            return _session.CreateQuery(queryString).List<Customer>();

        }
        
        public IList<Customer> GetCustomerByFirstnameWithParameters(string firstname)
        {

            const string queryString = "select c from Customer c where c.Firstname=:name";

            return _session.CreateQuery(queryString).SetString("name", firstname).List<Customer>();
        }

        public IList<Customer> GetCustomerByFirstnameAndLastname(string firstname, string lastname)
        {

            const string queryString = "select c from Customer c where c.Firstname=:fName and c.Lastname=:lName";

            return _session.CreateQuery(queryString)
                .SetString("fName", firstname)
                .SetString("lName", lastname)
                .List<Customer>();
        }

        public IList<Customer> GetCustomersWithIdGreaterThan(int id)
        {

            const string queryString = "select c from Customer c where c.Id > :id";

            return _session.CreateQuery(queryString).SetInt32("id", id).List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetCustomerByFirstName(string firstname)
        {

            return _session.CreateCriteria(typeof(Customer))
                .Add(Restrictions.Eq("Firstname", firstname))
                .List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetCustomersByFirstNameAndLastName(string firstname, string lastname)
        {

            return _session.CreateCriteria(typeof(Customer))
                .Add(Restrictions.Eq("Firstname", firstname))
                .Add(Restrictions.Eq("Lastname",lastname))
                .List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetCustomersWithIdGreaterThan(int id)
        {

            return _session.CreateCriteria(typeof(Customer))
                .Add(Restrictions.Gt("Id", id))
                .List<Customer>();
        }

        public IList<Customer> QueryByExample_GetCustomerByExample(Customer customerSample)
        {

            return _session.CreateCriteria(typeof(Customer))
                .Add(Example.Create(customerSample))
                .List<Customer>();
        }

        public IList<string> GetDistinctCustomerFirstNames()
        {
            const string queryString = "select distinct c.Firstname from Customer c";

            return _session.CreateQuery(queryString).List<string>();
        }

        public IList<string> CriteriaAPI_GetDistinctCustomerFirstNames()
        {


            return _session.CreateCriteria(typeof (Customer))
                .SetProjection(Projections.Distinct(Projections.Property("Firstname")))
                .List<string>();
        }

        public IList<Customer> GetCustomersOrderByLastname()
        {
            const string queryString = "select c from Customer c order by c.Lastname";

            return _session.CreateQuery(queryString).List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetCustomersOrderByLastname()
        { 
            return _session.CreateCriteria(typeof (Customer))
                .AddOrder(new Order("Lastname", true))
                .List<Customer>();
        }

        public IList<CustomerFirstnameCounter> GetCustomersFirstnameCount()
        {
            const string queryString = "select new CustomerFirstnameCounter(c.Firstname, count(c.Firstname)) from Customer c group by c.Firstname";

            var toReturn =  _session.CreateQuery(queryString).List<CustomerFirstnameCounter>();
            
            return toReturn;
        }

        public int AddCustomer(Customer customer)
        {
            object id = _session.Save(customer);
            _session.Flush();
            return (int) id;

        }

        public void DeleteCustomer(Customer customer)
        {
             _session.Delete(customer);
            _session.Flush();
        }

        public void UpdateCustomerFirstname(int customerId, string firstsname)
        {
            var currentCustomer = GetCustomerById(customerId);
            currentCustomer.Firstname = firstsname;

            _session.Update(currentCustomer);
            _session.Flush();
        }

        public void UpdateCustomerLastname(int customerId, string lastname)
        {
            var currentCustomer = GetCustomerById(customerId);
            currentCustomer.Lastname = lastname;

            _session.Update(currentCustomer);
            _session.Flush();
        }

        public void UpdateCustomer(Customer customer)
        {
            _session.Update(customer);
            _session.Flush();
        }


    }
}