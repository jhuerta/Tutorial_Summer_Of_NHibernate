using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class Product
    {
        private int _id;
        private string _name;
        private float _cost;
        private IList<Order> _orders;
        private int _version;

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual float Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        public virtual int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public virtual IList<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
    }
}
