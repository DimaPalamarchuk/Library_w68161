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
            bookSearch.Show();
            Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_BookShelf(object sender, RoutedEventArgs e)
        {
            Bookshelf bookshelf = new Bookshelf(currentUser);
            bookshelf.Show();
            Close();
        }
    }
}