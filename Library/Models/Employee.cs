using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Employee
    {
        public int id { get; set; }
        private string firstName, lastName;
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

        public Employee() { }

        public Employee(string login, string email, string pass)
        {
            this.login = login;
            this.email = email;
            this.pass = pass;
        }

    }
}


/*
CREATE TABLE "Employees" (
	"id"	INTEGER NOT NULL UNIQUE,
	"firstName"	TEXT NOT NULL,
	"lastName"	TEXT NOT NULL,
	"login"	TEXT NOT NULL,
	"pass"	TEXT NOT NULL,
	"email"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
*/