using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace Library
{
    public partial class BookSearch : Window
    {
        private string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        public BookSearch()
        {
            InitializeComponent();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            string selectedField = (cbSearchField.SelectedItem as ComboBoxItem).Content.ToString();
            string searchTerm = tbSearchTerm.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT title, author, available FROM Books WHERE {selectedField} LIKE '%{searchTerm}%';";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
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

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Navigation navigation = new Navigation();
            navigation.Show();
            Close();
        }
    }
}