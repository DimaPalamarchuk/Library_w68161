using Library;
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
    public partial class AuthEmployees : Window
    {
        public AuthEmployees()
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

                Employee authEmployee = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authEmployee = db.Employees.FirstOrDefault(em => em.Login == login && em.Pass == pass);
                }

                if (authEmployee != null)
                {
                    MessageBox.Show("Successful!");

                    AdminNavigation adminNavigation = new AdminNavigation();
                    adminNavigation.Show();
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
            Close();
        }

        private void Button_Login_Visitor(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}