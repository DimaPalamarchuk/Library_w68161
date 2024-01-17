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
            Hide();

        }
    }
}