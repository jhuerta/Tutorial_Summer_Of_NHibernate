using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace DataAccessLayer
{
    public class StaticSessionManager
    {
        public static readonly ISessionFactory SessionFactory;

        static StaticSessionManager()
        {
            try
            {
                var configuration = new Configuration();

                if (SessionFactory != null)
                {
                    throw new Exception("Trying to init SessionFactory twice!");
                }

                SessionFactory = configuration.Configure().BuildSessionFactory();

            }
            catch (Exception ex)
            {
                
                Console.Error.WriteLine(ex);
                throw new Exception("NHibernate initialization failed!", ex);
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
