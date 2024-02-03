using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library
{
    public partial class UserPageWindow : Window
    {
        public UserPageWindow()
        {
            InitializeComponent();

            ApplicationContext db = new ApplicationContext();
            List<User> users = db.Users.ToList();
            List<Book> books = db.Books.ToList();

            listOfUsers.ItemsSource = users;
            listOfBooks.ItemsSource = books;
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            AdminNavigation adminNavigation = new AdminNavigation();
            adminNavigation.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminNavigation.Show();
            Close();
        }
    }
}
