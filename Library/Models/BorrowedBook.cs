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


/*
    CREATE TABLE "BorrowedBooks" (
	"id"	INTEGER NOT NULL UNIQUE,
	"bookName"	TEXT NOT NULL,
	"userName"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("userName") REFERENCES "Users"("login"),
	FOREIGN KEY("bookName") REFERENCES "Books"("title")
);
*/