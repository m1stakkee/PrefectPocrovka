using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
using WpfApp2.Models;


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

        public object DgUsers { get; private set; }

        public AdminWindow()
        {
            
            InitializeComponent();
            LoadDataUsers(); 
            DataContext = this;
            
        }


        private void LoadDataUsers()
        {
            
            try
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();

                string query = "SELECT * FROM Users"; 
                _dataAdapter = new SqlDataAdapter(query, _sqlConnection);
                _dataTable = new DataTable();
                _dataAdapter.Fill(_dataTable);

                
                AdminWin.ItemsSource = _dataTable.DefaultView;

                
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

      
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_dataTable.GetChanges() != null) 
                {
                    _dataAdapter.Update(_dataTable);
                    _dataTable.AcceptChanges(); 
                    MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DBConcurrencyException ex) 
            {
                MessageBox.Show("Конфликт параллелизма. Данные были изменены другим пользователем.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

               
                _dataTable.RejectChanges();
                LoadDataUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (AdminWin.SelectedItem != null) 
            {
                
                DataRowView selectedRow = (DataRowView)AdminWin.SelectedItem;
                
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {

                        selectedRow.Delete(); 
                        _dataAdapter.Update(_dataTable);      
                        MessageBox.Show("Запись удалена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (DBConcurrencyException ex) 
                    {
                        MessageBox.Show("Конфликт параллелизма. Данные были изменены другим пользователем.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                        
                        _dataTable.RejectChanges();
                        LoadDataUsers();
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

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxbFurstName.Text) ||
                string.IsNullOrWhiteSpace(TxbSurName.Text) ||
                string.IsNullOrWhiteSpace(TxbLastName.Text) ||
                string.IsNullOrWhiteSpace(TxbLogin.Text) ||
                string.IsNullOrWhiteSpace(TxbPhone.Text) ||
                string.IsNullOrWhiteSpace(PsbPassword.Password) ||
                string.IsNullOrWhiteSpace(PsbPasswordRepeat.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PsbPassword.Password != PsbPasswordRepeat.Password)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataRow newRow = _dataTable.NewRow();
            newRow["FurstName"] = TxbFurstName.Text;
            newRow["SurName"] = TxbSurName.Text;
            newRow["LastName"] = TxbLastName.Text;
            newRow["Login"] = TxbLogin.Text;
            newRow["Phone"] = TxbPhone.Text;
            newRow["Password"] = PsbPassword.Password;
            

            _dataTable.Rows.Add(newRow);

            TxbFurstName.Clear();
            TxbSurName.Clear();
            TxbLastName.Clear();
            TxbLogin.Clear();
            TxbPhone.Clear();
            PsbPassword.Clear();
            PsbPasswordRepeat.Clear();

            SaveChanges();

            

        }
        private void SaveChanges()
        {
            try
            {
                if (_dataTable.GetChanges() != null)
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_dataAdapter); 
                    _dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();  
                    _dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                    _dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                    _dataAdapter.Update(_dataTable);
                    _dataTable.AcceptChanges();
                    MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Нет изменений для сохранения.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DBConcurrencyException ex)
            {
                MessageBox.Show("Конфликт параллелизма. Данные были изменены другим пользователем.\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _dataTable.RejectChanges();
                LoadDataUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }

   
}

