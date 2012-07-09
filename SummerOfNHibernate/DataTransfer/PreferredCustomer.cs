using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class PreferredCustomer : Customer
    {
        private DateTime _customerSince;
        private float _orderDiscountRate;

        public virtual DateTime CustomerSince
        {
            get { return _customerSince; }
            set { _customerSince = value; }
        }

        public virtual float OrderDiscountRate
        {
            get { return _orderDiscountRate; }
            set { _orderDiscountRate = value; }
        }
    }
}
