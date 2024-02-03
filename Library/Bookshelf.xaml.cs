using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Data.Entity;
using System.Data.SQLite;

namespace Library
{
    public partial class Bookshelf : Window
    {
        private string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        private User currentUser;

        ApplicationContext db;

        public Bookshelf(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadBooks();
            db = new ApplicationContext();
        }

        private void LoadBooks()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, title, author, available FROM Books;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgBookshelf.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Borrow(object sender, RoutedEventArgs e)
        {
            DataRowView selectedBook = dgBookshelf.SelectedItem as DataRowView;

            if (selectedBook != null)
            {
                try
                {
                    string bookTitle = selectedBook["title"].ToString();
                    int currentAvailable = Convert.ToInt32(selectedBook["available"]);

                    if (currentAvailable > 0)
                    {
                        AddBorrowedBook(bookTitle, currentUser.Login);
                        UpdateBookAvailabilityLocal(selectedBook, -1);

                        MessageBox.Show($"Book '{bookTitle}' borrowed by {currentUser.Login}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Book '{bookTitle}' is not available for borrowing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error borrowing book: {ex.Message}");
                    MessageBox.Show($"An error occurred while borrowing the book. See the console for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to borrow.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Return(object sender, RoutedEventArgs e)
        {
            DataRowView selectedBook = dgBookshelf.SelectedItem as DataRowView;

            if (selectedBook != null)
            {
                int bookId = Convert.ToInt32(selectedBook["id"]);
                string bookTitle = selectedBook["title"].ToString();

                BorrowedBook borrowedBook = db.BorrowedBooks.FirstOrDefault(bb => bb.BookName == bookTitle && bb.UserName == currentUser.Login);

                if (borrowedBook != null)
                {
                    UpdateBookAvailabilityLocal(selectedBook, 1);
                    MessageBox.Show($"Book '{bookTitle}' returned. Thank you!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    db.BorrowedBooks.Remove(borrowedBook);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show($"You can only return books that you borrowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to return.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBookAvailabilityLocal(DataRowView selectedBook, int change)
        {
            try
            {
                int currentIndex = dgBookshelf.Items.IndexOf(selectedBook);
                DataRowView updatedBook = dgBookshelf.Items[currentIndex] as DataRowView;
                int currentAvailable = Convert.ToInt32(updatedBook["available"]);


                updatedBook["available"] = Math.Max(0, currentAvailable + change);

                int bookId = Convert.ToInt32(updatedBook["id"]);
                Book book = db.Books.FirstOrDefault(b => b.id == bookId);
                if (book != null)
                {
                    book.Available = Convert.ToInt32(updatedBook["available"]);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book availability: {ex.Message}");
                MessageBox.Show($"An error occurred while updating book availability. See the console for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private User GetUserByLogin(string login)
        {
            return db.Users.FirstOrDefault(u => u.Login == login);
        }

        private void AddBorrowedBook(string bookTitle, string userLogin)
        {
            try
            {
                Book book = db.Books.FirstOrDefault(b => b.Title == bookTitle);

                if (book != null)
                {
                    BorrowedBook borrowedBook = new BorrowedBook
                    {
                        BookName = book.Title,
                        UserName = GetUserByLogin(userLogin)?.Login
                    };

                    db.BorrowedBooks.Add(borrowedBook);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show($"Book with title '{bookTitle}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding borrowed book: {ex.Message}");
                MessageBox.Show($"An error occurred while adding the borrowed book. See the console for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReturnBook(string bookTitle)
        {
            try
            {
                BorrowedBook borrowedBook = db.BorrowedBooks.FirstOrDefault(bb => bb.BookName == bookTitle);

                if (borrowedBook != null)
                {
                    db.BorrowedBooks.Remove(borrowedBook);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show($"No record found for book '{bookTitle}' in BorrowedBooks.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error returning book: {ex.Message}");
                MessageBox.Show($"An error occurred while returning the book. See the console for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Navigation navigation = new Navigation(currentUser);
            navigation.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            navigation.Show();
            Close();
        }

    }
}