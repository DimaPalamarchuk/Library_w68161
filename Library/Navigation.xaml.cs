using System.Windows;

namespace Library
{
    public partial class Navigation : Window
    {
        private User currentUser;

        public Navigation(string username = "")
        {
            InitializeComponent();
        }

        public Navigation(User currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }

        private void Button_Search_Book_Click(object sender, RoutedEventArgs e)
        {
            BookSearch bookSearch = new BookSearch();
            bookSearch.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bookSearch.Show();
            Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            Close();
        }

        private void Button_BookShelf(object sender, RoutedEventArgs e)
        {
            Bookshelf bookshelf = new Bookshelf(currentUser);
            bookshelf.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bookshelf.Show();
            Close();
        }
    }
}