using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using NHibernate;
using NHibernate.Context;

namespace WebFormHarness
{
    public class NHibernateSessionPerRequestModule : IHttpModule
    {
        #region Implementation of IHttpModule

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(Application_BeginRequest);
            context.EndRequest += new EventHandler(Application_EndRequest);
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(StaticSessionManager.SessionFactory);
            if(session!=null)
            {
                try
                {
                    session.Transaction.Commit();
                }
                catch (Exception)
                {
                    
                    session.Transaction.Rollback();
                }
                finally
                {
                    session.Close();
                }
            }
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            ISession session = StaticSessionManager.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
