using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DataTransfer
{
    public class Customer
    {
        //private string _firstname;
        //private string _lastname;
        private int _id;
        private Name _name;
        private int _version;

        private ISet<Order> orders;
        
        //public virtual string Firstname
        //{
        //    get { return _firstname; }
        //    set { _firstname = value; }
        //}

        //public virtual string Lastname
        //{
        //    get { return _lastname; }
        //    set { _lastname = value; }
        //}

        public virtual int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public virtual ISet<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public virtual Name Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual bool Equals(Customer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._name.Firstname, _name.Firstname) && Equals(other._name.Lastname, _name.Lastname);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Customer)) return false;
            return Equals((Customer) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_name.Firstname != null ? _name.Firstname.GetHashCode() : 0) * 397) ^ (_name.Lastname != null ? _name.Lastname.GetHashCode() : 0);
            }
        }
    }
}
