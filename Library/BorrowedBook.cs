namespace Library
{
    public class BorrowedBook
    {
        public int id { get; set; }
        private string bookName, userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string BookName
        {
            get { return bookName; }
            set { bookName = value; }
        }
    }
}
