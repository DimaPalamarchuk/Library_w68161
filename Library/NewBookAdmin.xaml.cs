using System;
using System.Data.SQLite;
using System.Windows;

namespace Library
{
    public partial class NewBook : Window
    {
        private readonly string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        public NewBook()
        {
            InitializeComponent();
        }

        private void Button_Click_AddBook(object sender, RoutedEventArgs e)
        {
            string title = tbTitle.Text.Trim();
            string author = tbAuthor.Text.Trim();
            string availableText = tbAvailable.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(availableText) || !int.TryParse(availableText, out int available))
            {
                MessageBox.Show("Please enter valid information for all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Books (title, author, available) VALUES (@title, @author, @available);";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", title);
                        command.Parameters.AddWithValue("@author", author);
                        command.Parameters.AddWithValue("@available", available);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Book added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            tbTitle.Clear();
            tbAuthor.Clear();
            tbAvailable.Clear();
        }
    }
}
