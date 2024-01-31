namespace Library
{
    public class User
    {
        public int id { get; set; }

        private string login, email, pass;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public User() { }

        public User(string login, string email, string pass)
        {
            this.login = login;
            this.email = email;
            this.pass = pass;
        }
        public void DeleteOldPassword(string newPass)
        {
            string oldPassword = Pass;

            if (oldPassword != null && oldPassword != newPass)
            {
            }

            Pass = newPass;
        }
        public override string ToString()
        {
            return "User: " + Login + " Email: " + Email;
        }
    }
}


/*
CREATE TABLE "Users" (
	"id"	INTEGER NOT NULL UNIQUE,
	"login"	TEXT NOT NULL,
	"pass"	TEXT NOT NULL,
	"email"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
*/