using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Library
{
    public partial class AuthWindow : Window
    {

        ApplicationContext db;
        public AuthWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

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
                authUser = db.Users.Where(u => u.Login == login && u.Pass == pass).FirstOrDefault();

                if (authUser != null)
                {
                    
                    MessageBox.Show("Successful!");
                    Navigation navigation = new Navigation(authUser);
                    navigation.Show();
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

        private void Button_ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordDialog forgotPasswordDialog = new ForgotPasswordDialog();
            if (forgotPasswordDialog.ShowDialog() == true)
            {
                string email = forgotPasswordDialog.Email;
                string login = forgotPasswordDialog.Login;

                User user;
                user = db.Users.FirstOrDefault(b => b.Email == email && b.Login == login);
                

                if (user != null)
                {
                    NewPasswordWindow newPasswordWindow = new NewPasswordWindow(user);
                    newPasswordWindow.Show();
                }
                else
                {
                    MessageBox.Show("User with the provided email and login not found.");
                }
            }
        }

        private void Button_Login_Employees(object sender, RoutedEventArgs e)
        {
            AuthEmployees authEmployees = new AuthEmployees();
            authEmployees.Show();
            Close();
        }
    }
}