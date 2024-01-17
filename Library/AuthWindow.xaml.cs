using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Library
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login.Length < 3)
            {
                textBoxLogin.ToolTip = "This field was entered incorrectly!";
                textBoxLogin.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passBox.ToolTip = "This field was entered incorrectly!";
                passBox.Background = Brushes.DarkRed;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;

                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();
                }

                if (authUser != null)
                {
                    MessageBox.Show("Successful!");

                    if (login == "Admin")
                    {
                        AdminNavigation adminNavigation = new AdminNavigation();
                        adminNavigation.Show();
                    }
                    else
                    {
                        Navigation navigation = new Navigation(login);
                        navigation.Show();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("You entered something incorrectly!");
                }
            }
        }


        private void Button_Window_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}