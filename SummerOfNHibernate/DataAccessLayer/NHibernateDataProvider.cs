using System;
using System.Linq;
using System.Collections.Generic;
using DataTransfer;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Order = DataTransfer.Order;

namespace DataAccessLayer
{
    public class NHibernateDataProvider
    {
        private ISession _session;


        public NHibernateDataProvider(ISession session)
        {
            _session = session;
        }

        public ISession Session
        {
            get { return _session; }
            set { _session = value; }
        }

        public Customer GetCustomerByIdNoUsingsession(int customerId)
        {
            return _session.Get<Customer>(customerId);
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customer = _session.Get<Customer>(customerId);
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

        private Customer GetCustomerByIdWithinOpenedTransaction(int customerId)
        {

            var customer = _session.Get<Customer>(customerId);
            return customer;
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
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var queryString = string.Format("select c from Customer c where c.Firstname = '{0}'", firstname);
                    var customers = _session.CreateQuery(queryString).List<Customer>();
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

        public IList<Customer> GetCustomerByFirstnameWithParameters(string firstname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select c from Customer c where c.Firstname=:name";
                    var customer = _session.CreateQuery(queryString).SetString("name", firstname).List<Customer>();
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

        public IList<Order> GetOrdersByJoiningCustomersAndOrderByCustomerAndDate(string firstname, DateTime date)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select distinct elements(c.Orders) from Customer c , elements(c.Orders) as o where o.OrderDate > :orderDate";
                    var orders = _session.CreateQuery(queryString).SetDateTime("orderDate", date).List<Order>();

                    transaction.Commit();

                    var justDirtyLazyLoadingCustomerFields = orders.Select(s => new
                                                                 {
                                                                     s.Customer.Firstname,
                                                                     s.Customer.Lastname,
                                                                     s.Customer.Id
                                                                 }
                        ).Count();

                    return orders;
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public IList<Customer> GetCustomersByJoiningCustomersAndOrderByCustomerAndDate(string firstname, DateTime date)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select distinct c from Customer c , elements(c.Orders) as o where o.OrderDate > :orderDate";
                    var customers = _session.CreateQuery(queryString).SetDateTime("orderDate", date).List<Customer>();
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

        public IList<Customer> GetCustomerByFirstnameAndLastname(string firstname, string lastname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select c from Customer c where c.Firstname=:fName and c.Lastname=:lName";

                    var customer = _session.CreateQuery(queryString).SetString("fName", firstname).SetString("lName", lastname).List<Customer>();
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

        public IList<Customer> GetCustomersWithIdGreaterThan(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select c from Customer c where c.Id > :id";

                    var customers = _session.CreateQuery(queryString).SetInt32("id", id).List<Customer>();
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

        public IList<Customer> CriteriaAPI_GetCustomerByFirstName(string firstname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customer = _session.CreateCriteria(typeof(Customer)).Add(Restrictions.Eq("Firstname", firstname)).List<Customer>();
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

        public IList<Customer> CriteriaAPI_GetCustomersByFirstNameAndLastName(string firstname, string lastname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customers = _session.CreateCriteria(typeof(Customer)).Add(Restrictions.Eq("Firstname", firstname)).Add(Restrictions.Eq("Lastname", lastname)).List<Customer>();
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

        public IList<Customer> CriteriaAPI_GetCustomersWithIdGreaterThan(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customers = _session.CreateCriteria(typeof(Customer)).Add(Restrictions.Gt("Id", id)).List<Customer>();
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

        public IList<Customer> QueryByExample_GetCustomerByExample(Customer customerSample)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customer = _session.CreateCriteria(typeof(Customer)).Add(Example.Create(customerSample)).List<Customer>();
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

        public IList<string> GetDistinctCustomerFirstNames()
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select distinct c.Firstname from Customer c";
                    var customerFirstNames = _session.CreateQuery(queryString)
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

        public IList<string> CriteriaAPI_GetDistinctCustomerFirstNames()
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customers = _session.CreateCriteria(typeof(Customer)).SetProjection(Projections.Distinct(Projections.Property("Firstname"))).List<string>();
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

        public IList<Customer> GetCustomersOrderByLastname()
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select c from Customer c order by c.Lastname";

                    var customer = _session.CreateQuery(queryString)
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

        public IList<Customer> CriteriaAPI_GetCustomersOrderByLastname()
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customers = _session.CreateCriteria(typeof(Customer))
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

        public IList<CustomerFirstnameCounter> GetCustomersFirstnameCount()
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    const string queryString = "select new CustomerFirstnameCounter(c.Firstname, count(c.Firstname)) from Customer c group by c.Firstname";
                    var toReturn = _session.CreateQuery(queryString).List<CustomerFirstnameCounter>();
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

        public int AddCustomer(Customer customer)
        {
            using (var trasaction = _session.BeginTransaction())
            {
                try
                {
                    var id = _session.Save(customer);
                    _session.Flush();
                    trasaction.Commit();
                    return (int)id;
                }
                catch (HibernateException)
                {
                    trasaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(customer);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteCustomerWithTransactionCanRollBack(Customer undeletableCustomer, Customer deletableCustomer)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(deletableCustomer);
                    _session.Flush();

                    _session.Delete(undeletableCustomer);
                    _session.Flush();

                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteCustomerWithTransaction(Customer customer)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(customer);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateCustomerFirstname(int customerId, string firstsname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var currentCustomer = GetCustomerByIdWithinOpenedTransaction(customerId);
                    currentCustomer.Firstname = firstsname;

                    _session.Update(currentCustomer);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateCustomerLastname(int customerId, string lastname)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var currentCustomer = GetCustomerByIdWithinOpenedTransaction(customerId);
                    currentCustomer.Lastname = lastname;

                    _session.Update(currentCustomer);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Update(customer);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Customer GetCustomerAndOrdersByCustomerId(int customerId)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customer = _session.Get<Customer>(customerId);
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

        public void SaveOrUpdateCustomers(IList<Customer> customers)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    foreach (var customer in customers)
                    {
                        _session.SaveOrUpdate(customer);
                    }
                    _session.Flush();
                    transaction.Commit();
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        public IList<Customer> GetDistinctCustomersWithOrdersSince(DateTime orderDate)
        {
            return _session.CreateQuery("select distinct c from Customer c, elements(c.Orders) as o where o.OrderDate > :orderDate").
                SetDateTime("orderDate", orderDate).List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetDistinctCustomersWithOrdersSince(DateTime orderDate)
        {
            // The distinct is done in software, no by query! And is done in this server
            return
                _session.CreateCriteria(typeof(Customer))
                    .CreateCriteria("Orders")
                    .Add(Expression.Gt("OrderDate", orderDate))
                    .SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                    .List<Customer>();
        }

        public IList<Customer> CriteriaAPI_GetDistinctCustomersWithOrdersSinceWithProjects(DateTime orderDate)
        {
            // The projection is forcing to do the query in the database, in the SQL server
            var ids = _session.CreateCriteria(typeof(Customer))
                .SetProjection(
                    Projections.Distinct(
                        Projections.ProjectionList()
                        .Add(Projections.Property("Id")))
                        )
                        .CreateCriteria("Orders")
                        .Add(Expression.Gt("OrderDate", orderDate))
                        .SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer())
                        .List<int>();
            return
                _session.CreateCriteria(typeof (Customer))
                .Add(Expression.In("Id", ids.ToList()))
                .List<Customer>();
        }

        public IList<Customer> GetCustomersWithORdersHavingProduct(int productId)
        {

            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customers = _session.CreateCriteria(typeof (Customer))
                        .CreateCriteria("Orders")
                        .CreateCriteria("Products")
                        .Add(Expression.Eq("Id", productId)).List<Customer>();
                    //_session.Flush();
                    //transaction.Commit();
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


}