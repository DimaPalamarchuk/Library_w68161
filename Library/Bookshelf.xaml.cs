using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public partial class Bookshelf : Window
    {
        private readonly string connectionString = "Data Source=LibraryDataBase.db;Version=3;";
        private ObservableCollection<Book> books;

        public Bookshelf()
        {
            InitializeComponent();
            LoadBooks();
            dgBookshelf.ItemsSource = books;
        }

        private void LoadBooks()
        {
            books = new ObservableCollection<Book>();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Books;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string title = reader["title"].ToString();
                                string author = reader["author"].ToString();
                                int available = Convert.ToInt32(reader["available"]);

                                books.Add(new Book { Title = title, Author = author, Available = available });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ... (ваш код)

        private void Button_Click_BorrowBook(object sender, RoutedEventArgs e)
        {
            if (dgBookshelf.SelectedItem != null)
            {
                Book selectedBook = (Book)dgBookshelf.SelectedItem;

                if (selectedBook.Available > 0)
                {
                    // Уменьшаем значение Available на 1
                    selectedBook.Available--;

                    // Обновляем отображение в DataGrid
                    dgBookshelf.Items.Refresh();

                    // Теперь вы можете добавить код для записи изменений в базу данных,
                    // чтобы сохранить уменьшение значения Available.
                    // Обновите вашу базу данных в соответствии с изменением состояния книги.

                    // Пример:
                    UpdateBookAvailabilityInDatabase(selectedBook.Title, selectedBook.Available);
                }
                else
                {
                    MessageBox.Show("This book is not available.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void UpdateBookAvailabilityInDatabase(string bookTitle, int newAvailability)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Books SET available = @newAvailability WHERE title = @bookTitle;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newAvailability", newAvailability);
                        command.Parameters.AddWithValue("@bookTitle", bookTitle);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            // Обновление в базе данных выполнено успешно
                        }
                        else
                        {
                            MessageBox.Show("Failed to update book availability in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ... (ваш код)


        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Available { get; set; }
        public int UserId { get; set; }
    }
}
