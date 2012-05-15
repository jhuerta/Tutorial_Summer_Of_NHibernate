namespace DataTransfer
{
    public class CustomerFirstnameCounter
    {
        private string _firstname;
        private long _count;

        public CustomerFirstnameCounter(string firstname, long count)
        {
            _firstname = firstname;
            _count = count;
        }

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public long Count
        { 
            get { return _count; }
            set { _count = value; }
        }
    }
}