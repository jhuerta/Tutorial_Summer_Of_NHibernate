using DataTransfer;
using NHibernate.Cfg;

namespace DataAccessLayer
{
    public class NHibernateDataProvider
    {
        public Customer GetCustomerById(int customerId)
        {
            // 1. Get config file
            // 2. Factory will build session with the config file
            // 3. Both factory and config file are disposable
            // Therefore: We can combine and not stored variables
            //var config = new Configuration();
            //config.Configure();
            //var sessionFactory = config.BuildSessionFactory();
            //var session = sessionFactory.OpenSession();

            var configuration = new Configuration();
            configuration.Configure("hibernate.cfg.xml");
            var sessionFactory = configuration.BuildSessionFactory();

            var session = sessionFactory.OpenSession();

            // This would work but the risk is that it will try to cast the return into an object
            // This cast operation may fail.
            //return (DataTransfer.Customer) session.Get(typeof (DataTransfer.Customer), customerId);

            // Because the cast may fail, we'd rather go for  generics.
            return session.Get<Customer>(customerId);

        }

    }
}

