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
    public partial class NewPasswordWindow : Window
    {
        private User user;

        public NewPasswordWindow(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = passwordBox.Password;
            string confirmNewPassword = confirmPasswordBox.Password;

            if (newPassword.Length < 5)
            {
                MessageBox.Show("Password should be at least 5 characters long.");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                User userToUpdate = db.Users.FirstOrDefault(u => u.Login == user.Login);

                if (userToUpdate != null)
                {
                    userToUpdate.Pass = newPassword;
                    db.SaveChanges();

                    MessageBox.Show("Password updated successfully!");
                    Close();
                }
                else
                {
                    MessageBox.Show("User not found in the database.");
                }
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}