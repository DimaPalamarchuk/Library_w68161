using Library;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public ApplicationContext() : base("DefaultConnection") { }
    }
}


