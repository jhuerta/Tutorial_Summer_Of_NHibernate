using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using DataTransfer;

namespace WebFormHarness
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NHibernateDataProvider provider = new NHibernateDataProvider(StaticSessionManager.SessionFactory.GetCurrentSession());
 

            if(!IsPostBack)
            {
                IList<Customer> customers = provider.CriteriaAPI_GetCustomerByFirstName("Juan");
                Session["Juans"] = customers;
                Response.Write(String.Format("Count of Customers Found = {0}",customers.Count));
            }

            if(IsPostBack)
            {
                IList<Customer> retrievedCustomers = Session["Juans"] as IList<Customer>;

                NHibernate.ISession compareSession = StaticSessionManager.SessionFactory.GetCurrentSession();

                foreach (var customer in retrievedCustomers)
                {
                    Response.Write(string.Format("<br/>{0}, {1} (in session = {2})",
                        customer.Name.Firstname,
                        customer.Name.Lastname,
                        compareSession.Contains(customer).ToString()));
                }

                //NhiberanteSessionPerConverstationMOdule.EndConverstation();
            }

        }
    }
}
