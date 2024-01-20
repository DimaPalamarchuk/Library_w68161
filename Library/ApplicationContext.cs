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

        public ApplicationContext() : base("DefaultConnection") { }
    }
}


