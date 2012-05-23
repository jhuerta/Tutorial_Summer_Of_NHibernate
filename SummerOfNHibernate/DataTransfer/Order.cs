using System;
using System.Collections.Generic;

namespace DataTransfer
{
    public class Order
    {
        private int _id;
        private DateTime _orderDate;
        private Customer _customer;
        private IList<Product> _products;

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        public virtual Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public virtual IList<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
    }
}
