using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTransfer;
using NHibernate;
using NHibernate.Criterion;
using Order = DataTransfer.Order;

namespace DataAccessLayer
{
    public class GenericDataProvider<T>
    {
        private ISession _session;

        public GenericDataProvider(ISession session)
        {
            _session = session;
        }

        public T GetById(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var t = _session.Get<T>(id);
                    transaction.Commit();
                    return t;
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        } 

        public IList<T> Find(DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(_session).List<T>();
        }
    }

    public class SuperOrderClassDataProvider : GenericDataProvider<Order>
    {
        public SuperOrderClassDataProvider(ISession session)
            : base(session)
        {
        }

        public Order GetOrderById(int orderId)
        {
            var nameCriteria =
                DetachedCriteria.For<Order>()
                    .Add(Expression.Eq("Id", orderId));

            return Find(nameCriteria).Single();
        }
    }

    public class SuperCustomerClassDataProvider : GenericDataProvider<Customer>
    {
        public SuperCustomerClassDataProvider(ISession session) : base(session)
        {
        }

        public IList<Customer> GetCustomerByName(Name name)
        {
            var nameCriteria =
                DetachedCriteria.For<Customer>()
                    .Add(Expression.Eq("Name.Firstname", name.Firstname))
                    .Add(Expression.Eq("Name.Lastname", name.Lastname));

            return Find(nameCriteria);
        }
    }
    public class CustomerDataProvider
    {
        private ISession _session;

        public CustomerDataProvider(ISession session)
        {
            _session = session;
        }

        public Customer GetCustomerById(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var customer = _session.Get<Customer>(id);
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

    public class OrderDataProvider
    {
        private ISession _session;

        public OrderDataProvider(ISession session)
        {
            _session = session;
        }

        public Order GetOrderById(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var order = _session.Get<Order>(id);
                    transaction.Commit();
                    return order;
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public class ProductDataProvider
    {
        private ISession _session;

        public ProductDataProvider(ISession session)
        {
            _session = session;
        }

        public Product GetProductById(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    var product = _session.Get<Product>(id);
                    transaction.Commit();
                    return product;
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
