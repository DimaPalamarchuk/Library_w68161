using System.Windows;


//Уже не используется
namespace Library
{
    public partial class InputDialog : Window
    {
        public string Answer
        {
            get { return txtAnswer.Text; }
        }

        public InputDialog(string question)
        {
            InitializeComponent();
            lblQuestion.Content = question;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
