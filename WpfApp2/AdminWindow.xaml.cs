using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private SqlDataAdapter _dataAdapter;
        private DataTable _dataTable;
        private SqlConnection _sqlConnection;
        private string _connectionString = @"Data Source=IVAN\SQLEXPRESS;Initial Catalog=prefect_pocrovskoe_streshnego;Integrated Security=True;Encrypt=False"; //Строка подключения

        public AdminWindow()
        {
            InitializeComponent();
            LoadData(); // Загружаем данные при открытии окна
        }

        private void LoadData()
        {
            try
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();

                string query = "SELECT * FROM Users"; // Получаем все данные из Polzovateli
                _dataAdapter = new SqlDataAdapter(query, _sqlConnection);
                _dataTable = new DataTable();
                _dataAdapter.Fill(_dataTable);

                // Привязываем данные к DataGrid
                AdminWin.ItemsSource = _dataTable.DefaultView;

                // Для поддержки добавления и удаления нужны SqlCommandBuilder
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_dataAdapter);
                _dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                _dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {

        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dataTable.GetChanges() != null) // Проверяем, были ли изменения
                {
                    _dataAdapter.Update(_dataTable);
                    _dataTable.AcceptChanges(); // Принимаем изменения
                    MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DBConcurrencyException ex) // Обрабатываем конфликты параллелизма
            {
                MessageBox.Show("Конфликт параллелизма. Данные были изменены другим пользователем.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                //Перезагрузка данных
                _dataTable.RejectChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Обработчик для кнопки удаления (если используете отдельную кнопку)
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (AdminWin.SelectedItem != null) // Проверяем что строка выбрана.
            {
                // Получаем выбранную строку
                DataRowView selectedRow = (DataRowView)AdminWin.SelectedItem;
                // Подтверждение удаления
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {

                        selectedRow.Delete(); // Помечаем строку как удаленную
                        _dataAdapter.Update(_dataTable); // Отправляем изменения в БД_dataTable.AcceptChanges();     // Подтверждаем изменения в DataTable
                        MessageBox.Show("Запись удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (DBConcurrencyException ex) // Обрабатываем конфликты параллелизма
                    {
                        MessageBox.Show("Конфликт параллелизма. Данные были изменены другим пользователем.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                        //Перезагрузка данных
                        _dataTable.RejectChanges();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении записи: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Закрытие соединения при закрытии окна.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
