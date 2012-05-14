using System;
using System.Collections.Generic;
using DataTransfer;
using NHibernate;
using NHibernate.Cfg;

namespace DataAccessLayer
{
    public class NHibernateDataProvider
    {

        public Customer GetCustomerById(int customerId)
        {
            var session = GetSession();

            return session.Get<Customer>(customerId);
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
            var session = GetSession();

            var queryString = string.Format("select c from Customer c where c.Firstname = '{0}'", firstname);

            return session.CreateQuery(queryString).List<Customer>();

        }
        
        public IList<Customer> GetCustomerByFirstnameWithParameters(string firstname)
        {
            var session = GetSession();

            const string queryString = "select c from Customer c where c.Firstname=:name";

            return session.CreateQuery(queryString).SetString("name",firstname).List<Customer>();
        }

        public IList<Customer> GetCustomerByFirstnameAndLastname(string firstname, string lastname)
        {
            var session = GetSession();

            const string queryString = "select c from Customer c where c.Firstname=:fName and c.Lastname=:lName";

            return session.CreateQuery(queryString)
                .SetString("fName", firstname)
                .SetString("lName", lastname)
                .List<Customer>();
        }

        public IList<Customer> GetCustomersWithIdGreaterThan(int id)
        {
            ISession session = GetSession();

            const string queryString = "select c from Customer c where c.Id > :id";

            return session.CreateQuery(queryString).SetInt32("id", id).List<Customer>();
        }
    }
}