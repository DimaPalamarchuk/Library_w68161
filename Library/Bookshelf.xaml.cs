using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace Library
{
    public partial class Bookshelf : Window
    {
        private string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        public Bookshelf()
        {
            InitializeComponent();

            LoadBookshelfData();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            // Добавьте код для возврата назад, если необходимо
        }

        private void LoadBookshelfData()
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
                            dgBookshelf.ItemsSource = dataTable.DefaultView;
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
