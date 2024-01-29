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

        public Bookshelf(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadBooks();
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
                    int bookId = Convert.ToInt32(selectedBook["id"]);
                    string bookTitle = selectedBook["title"].ToString();

                    string userInput = GetUserInput("Enter user login:");

                    if (!string.IsNullOrEmpty(userInput))
                    {
                        User user = GetUserByLogin(userInput);

                        if (user != null)
                        {
                            AddBorrowedBook(bookTitle, user.Login);
                            UpdateBookAvailabilityLocal(bookTitle, -1);

                            MessageBox.Show($"Book '{bookTitle}' borrowed by {user.Login}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show($"User with login '{userInput}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a user login.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                ReturnBook(bookTitle);

                UpdateBookAvailabilityLocal(bookTitle, 1);

                MessageBox.Show($"Book '{bookTitle}' returned. Thank you!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a book to return.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBookAvailabilityLocal(string bookTitle, int change)
        {
            DataRowView selectedBook = dgBookshelf.SelectedItem as DataRowView;

            if (selectedBook != null)
            {
                int currentIndex = dgBookshelf.Items.IndexOf(selectedBook);
                DataRowView updatedBook = dgBookshelf.Items[currentIndex] as DataRowView;
                int currentAvailable = Convert.ToInt32(updatedBook["available"]);
                updatedBook["available"] = Math.Max(0, currentAvailable + change);
            }
        }

        private string GetUserInput(string prompt)
        {
            InputDialog inputDialog = new InputDialog(prompt);
            if (inputDialog.ShowDialog() == true)
            {
                return inputDialog.Answer;
            }
            return null;
        }

        private User GetUserByLogin(string login)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Users.FirstOrDefault(u => u.Login == login);
            }
        }

        private void AddBorrowedBook(string bookTitle, string userLogin)
        {
            try
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    Book book = context.Books.FirstOrDefault(b => b.Title == bookTitle);

                    if (book != null)
                    {
                        BorrowedBook borrowedBook = new BorrowedBook
                        {
                            BookName = book.Title,
                            UserName = GetUserByLogin(userLogin)?.Login
                        };

                        context.BorrowedBooks.Add(borrowedBook);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Book with title '{bookTitle}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
                using (ApplicationContext context = new ApplicationContext())
                {
                    BorrowedBook borrowedBook = context.BorrowedBooks.FirstOrDefault(bb => bb.BookName == bookTitle);

                    if (borrowedBook != null)
                    {
                        context.BorrowedBooks.Remove(borrowedBook);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"No record found for book '{bookTitle}' in BorrowedBooks.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
            navigation.Show();
            Close();
        }

        private void dgBookshelf_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
