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
        // Строка подключения к базе данных SQLite
        private readonly string connectionString = "Data Source=LibraryDataBase.db;Version=3;";

        // Конструктор окна BookSearchAdmin
        public BookSearchAdmin()
        {
            InitializeComponent();
        }

        // Обработчик события клика на кнопке "Назад"
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            // Закрыть текущее окно и открыть окно AdminNavigation
            AdminNavigation adminNaviog = new AdminNavigation();
            adminNaviog.Show();
            Close();
        }

        // Обработчик события клика на кнопке "Поиск"
        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            // Получить поисковый запрос и поле поиска из пользовательского ввода
            string searchTerm = tbSearchTerm.Text.Trim();
            string searchField = (cbSearchField.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Проверить, предоставлены ли поле поиска и поисковый запрос
            if (searchField == null || searchTerm.Length == 0)
            {
                MessageBox.Show("Please select a search field and enter a search term.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Подключение к базе данных SQLite
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для поиска книг на основе ввода пользователя
                    string query = $"SELECT * FROM Books WHERE {searchField.ToLower()} LIKE '%' || @searchTerm || '%';";

                    // Выполнение запроса с использованием параметров для предотвращения SQL-инъекций
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchTerm", searchTerm);

                        // Использование DataAdapter для заполнения результатов в DataTable
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Установка DataTable в качестве источника данных для таблицы данных
                            dgSearchResults.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Вывод сообщения об ошибке на английском языке, если произошло исключение во время поиска
                MessageBox.Show("An error occurred: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик события клика на кнопке "Добавить книгу"
        private void Button_Click_AddBook(object sender, RoutedEventArgs e)
        {
            // Открыть окно NewBook для добавления новой книги
            NewBook newBook = new NewBook();
            newBook.Show();
        }
    }
}