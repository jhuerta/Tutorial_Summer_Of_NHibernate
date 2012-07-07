namespace DataTransfer
{
    public class Name
    {
        public override string ToString()
        {
            return _firstname + " " + _lastname;
        }

        private string _firstname;
        private string _lastname;

        public virtual string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public string Fullname
        {
            get { return _firstname + " " + _lastname; }
         }
    }
}
