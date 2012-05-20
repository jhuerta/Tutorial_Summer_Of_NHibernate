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
        private readonly ISessionFactory _sessionFactory;

        public NHibernateDataProvider()
        {
            _sessionFactory = GetSessionFactory();
        }

        public NHibernateDataProvider(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Customer GetCustomerByIdNoUsingsession(int customerId)
        {
            return GetSession().Get<Customer>(customerId);
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customer = session.Get<Customer>(customerId);
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory GetSessionFactory()
        {
            var configuration = new Configuration();

            configuration.Configure("hibernate.cfg.xml");

            return configuration.BuildSessionFactory();
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
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var queryString = string.Format("select c from Customer c where c.Firstname = '{0}'", firstname);
                        var customers = session.CreateQuery(queryString).List<Customer>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        public IList<Customer> GetCustomerByFirstnameWithParameters(string firstname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select c from Customer c where c.Firstname=:name";
                        var customer = session.CreateQuery(queryString).SetString("name", firstname).List<Customer>();
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> GetCustomerByFirstnameAndLastname(string firstname, string lastname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select c from Customer c where c.Firstname=:fName and c.Lastname=:lName";

                        var customer = session.CreateQuery(queryString).SetString("fName", firstname).SetString("lName", lastname).List<Customer>();
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> GetCustomersWithIdGreaterThan(int id)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select c from Customer c where c.Id > :id";

                        var customers = session.CreateQuery(queryString).SetInt32("id", id).List<Customer>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
        }

        public IList<Customer> CriteriaAPI_GetCustomerByFirstName(string firstname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customer = session.CreateCriteria(typeof (Customer)).Add(Restrictions.Eq("Firstname", firstname)).List<Customer>();
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> CriteriaAPI_GetCustomersByFirstNameAndLastName(string firstname, string lastname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customers = session.CreateCriteria(typeof (Customer)).Add(Restrictions.Eq("Firstname", firstname)).Add(Restrictions.Eq("Lastname", lastname)).List<Customer>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> CriteriaAPI_GetCustomersWithIdGreaterThan(int id)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customers = session.CreateCriteria(typeof (Customer)).Add(Restrictions.Gt("Id", id)).List<Customer>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> QueryByExample_GetCustomerByExample(Customer customerSample)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customer = session.CreateCriteria(typeof (Customer)).Add(Example.Create(customerSample)).List<Customer>();
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<string> GetDistinctCustomerFirstNames()
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select distinct c.Firstname from Customer c";
                        var customerFirstNames = session.CreateQuery(queryString)
                            .List<string>();
                        transaction.Commit();
                        return customerFirstNames;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<string> CriteriaAPI_GetDistinctCustomerFirstNames()
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customers = session.CreateCriteria(typeof (Customer)).SetProjection(Projections.Distinct(Projections.Property("Firstname"))).List<string>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> GetCustomersOrderByLastname()
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select c from Customer c order by c.Lastname";

                        var customer = session.CreateQuery(queryString)
                            .List<Customer>();
                        transaction.Commit();
                        return customer;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<Customer> CriteriaAPI_GetCustomersOrderByLastname()
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customers = session.CreateCriteria(typeof (Customer))
                            .AddOrder(new NHibernate.Criterion.Order("Lastname", true))
                            .List<Customer>();
                        transaction.Commit();
                        return customers;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IList<CustomerFirstnameCounter> GetCustomersFirstnameCount()
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        const string queryString = "select new CustomerFirstnameCounter(c.Firstname, count(c.Firstname)) from Customer c group by c.Firstname";
                        var toReturn = session.CreateQuery(queryString).List<CustomerFirstnameCounter>();
                        transaction.Commit();
                        return toReturn;
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public int AddCustomer(Customer customer)
        {
            using (var session = GetSession())
            {
                using (var trasaction = session.BeginTransaction())
                {
                    try
                    {
                        var id = session.Save(customer);
                        session.Flush();
                        trasaction.Commit();
                        return (int)id;
                    }
                    catch(HibernateException)
                    {
                        trasaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(customer);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }



        public void DeleteCustomerWithTransactionCanRollBack(Customer undeletableCustomer, Customer deletableCustomer)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(deletableCustomer);
                        session.Flush();

                        session.Delete(undeletableCustomer);
                        session.Flush();

                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteCustomerWithTransaction(Customer customer)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(customer);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateCustomerFirstname(int customerId, string firstsname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var currentCustomer = GetCustomerById(customerId);
                        currentCustomer.Firstname = firstsname;

                        session.Update(currentCustomer);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateCustomerLastname(int customerId, string lastname)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var currentCustomer = GetCustomerById(customerId);
                        currentCustomer.Lastname = lastname;

                        session.Update(currentCustomer);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(customer);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Customer GetCustomerAndOrdersByCustomerId(int customerId)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var customer = session.Get<Customer>(customerId);
                        NHibernateUtil.Initialize(customer.Orders);
                        return customer;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void SaveOrUpdateCustomers(IList<Customer> customers)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var customer in customers)
                        {
                            session.SaveOrUpdate(customer);
                        }
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (HibernateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}