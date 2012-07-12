using NHibernate;
using NHibernate.Cfg;

namespace DataAccessLayer
{
    public class NHibernateSessionManager
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionManager()
        {
            _sessionFactory = GetSessionFactory();
        }

        public ISession GetSession()
        {
            //return _sessionFactory.OpenSession();
            return _sessionFactory.OpenSession(new MyInterceptor());
        }

        private static ISessionFactory GetSessionFactory()
        {

            var configuration = new Configuration();

            configuration.Configure("hibernate.cfg.xml");

            return configuration.BuildSessionFactory();
        }
    }
}