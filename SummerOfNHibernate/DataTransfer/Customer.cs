namespace DataTransfer
{
    public class Customer
    {
        private string _firstname;
        private string _lastname;
        private int _id;

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
    }
}
