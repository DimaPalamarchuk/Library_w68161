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
        // Constructor to initialize with the user whose password is being reset
        public NewPasswordWindow(User user)
        {
            InitializeComponent();

            // You can use the provided user information if needed
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = passwordBox.Password;
            string confirmNewPassword = confirmPasswordBox.Password;

            if (newPassword.Length < 5)
            {
                // Handle validation, show error message, etc.
                MessageBox.Show("Password should be at least 5 characters long.");
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                // Handle validation, show error message, etc.
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // TODO: Add logic to update the user's password in the database

            MessageBox.Show("Password updated successfully!");
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
