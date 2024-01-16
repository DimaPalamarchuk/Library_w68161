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

    }
}