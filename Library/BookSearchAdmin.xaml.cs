using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public partial class BookSearchAdmin : Window
    {
        private readonly string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        public BookSearchAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {      
            AdminNavigation adminNaviog = new AdminNavigation();
            adminNaviog.Show();
            Close();

        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            string searchTerm = tbSearchTerm.Text.Trim();
            string searchField = (cbSearchField.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (searchField == null || searchTerm.Length == 0)
            {
                MessageBox.Show("Please select a search field and enter a search term.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM Books WHERE {searchField.ToLower()} LIKE '%' || @searchTerm || '%';";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", searchTerm);

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgSearchResults.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_AddBook(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.Show();
        }

    }
}
