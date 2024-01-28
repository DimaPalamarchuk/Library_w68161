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
    public partial class ForgotPasswordDialog : Window
    {
        public string Email { get; private set; }
        public string Login { get; private set; }

        public ForgotPasswordDialog()
        {
            InitializeComponent();
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            Email = textBoxEmail.Text.Trim();
            Login = textBoxLogin.Text.Trim();

            DialogResult = true;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}