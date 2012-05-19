namespace DataTransfer
{
    public class Customer
    {
        private string _firstname;
        private string _lastname;
        private int _id;
        private int _version;

        public virtual string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public virtual string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

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

        public virtual bool Equals(Customer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._firstname, _firstname) && Equals(other._lastname, _lastname);
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
                return ((_firstname != null ? _firstname.GetHashCode() : 0)*397) ^ (_lastname != null ? _lastname.GetHashCode() : 0);
            }
        }
    }
}
