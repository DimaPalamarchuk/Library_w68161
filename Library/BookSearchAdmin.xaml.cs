using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace Library
{
    public partial class BookSearchAdmin : Window
    {
        private string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        public BookSearchAdmin()
        {
            InitializeComponent();
            LoadBookData();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            // Добавьте код для возврата назад, если необходимо
        }

        private void Button_Click_AddBook(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Please enter both title and author.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Выполняем SQL-запрос для добавления новой книги в базу данных
                    string query = "INSERT INTO Books (title, author, available) VALUES (@title, @author, 1);";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@author", author);

                        command.ExecuteNonQuery();

                        // После добавления обновляем отображение списка книг
                        LoadBookData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadBookData()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Выполняем SQL-запрос для получения всех книг из базы данных
                    string query = "SELECT * FROM Books;";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Заполняем DataGrid данными из базы данных
                            dgBooks.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
