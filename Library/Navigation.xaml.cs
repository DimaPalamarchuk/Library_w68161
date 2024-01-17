using System.Windows;

namespace Library
{
    public partial class Navigation : Window
    {
        public Navigation(string username = "")
        {
            InitializeComponent();

            userName.Text = "User: " + username;
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
    }
}