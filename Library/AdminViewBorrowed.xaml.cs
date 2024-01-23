using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Library
{
    public partial class AdminViewBorrowed : Window
    {
        private ObservableCollection<BorrowedBook> borrowedBooks;

        public AdminViewBorrowed()
        {
            InitializeComponent();
            LoadBorrowedBooks();
        }

        private void LoadBorrowedBooks()
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    borrowedBooks = new ObservableCollection<BorrowedBook>(context.BorrowedBooks.ToList());
                    dgBorrowedBooks.ItemsSource = borrowedBooks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
