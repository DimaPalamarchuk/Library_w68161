namespace Library
{
    public class Book
    {
        public int id { get; set; }

        private string title, author;
        private int available;


        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public int Available
        {
            get { return available; }
            set { available = value; }
        }

        public Book() { }

        public Book(string title, string author, int available)
        {
            this.title = title;
            this.author = author;
            this.available = available;
            
        }

        public override string ToString()
        {
            return $"Book ID: {id}, Title: {title}, Author: {author}, Available: {available}";
        }
    }
}

/*
  CREATE TABLE "Books" (
	"id"	INTEGER NOT NULL UNIQUE,
	"title"	TEXT NOT NULL UNIQUE,
	"author"	TEXT NOT NULL,
	"available"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
*/