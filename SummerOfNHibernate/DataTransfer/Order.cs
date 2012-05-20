using System;

namespace DataTransfer
{
    public class Order
    {
        private int _orderId;
        private DateTime _orderDate;
        private Customer _customer;

        public virtual int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
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
    }
}
