using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class CustomersWithToys
    {
        private int _customerId;
        private string _firstname;
        private string _lastname;
        private int _orderId;
        private DateTime _orderDate;


        public virtual int CustomerId
        {
            get { return _customerId; }
            protected set { _customerId = value; }
        }

        public virtual string Firstname
        {
            get { return _firstname; }
            protected set { _firstname = value; }
        }

        public virtual string Lastname
        {
            get { return _lastname; }
            protected set { _lastname = value; }
        }

        public virtual int OrderId
        {
            get { return _orderId; }
            protected set { _orderId = value; }
        }

        public virtual DateTime OrderDate
        {
            get { return _orderDate; }
            protected set { _orderDate = value; }
        }
    }
}
