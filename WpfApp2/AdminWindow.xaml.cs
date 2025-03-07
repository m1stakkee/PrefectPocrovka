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
    
    public partial class AdminWindow : Window
    {



        private string _FurstName;
        private string _SurName;
        private string _LastName;
        private string _Phone;
        private string _Password;
        private string _RepeatPassword;
        private int _Roleid = 2;


        public AdminWindow()
        {
            InitializeComponent();
            LoadDataUsers();

            CmbRole.ItemsSource = ConnectDb.Connect.Roles.ToList();
        }

        private void LoadDataUsers()
        {
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _FurstName = TxbFurstName.Text;
                _SurName = TxbSurName.Text;
                _LastName = TxbLastName.Text;
                _Phone = TxbPhone.Text;
                _Password = PsbPassword.Password;
                _RepeatPassword = PsbPasswordRepeat.Password;
                _Roleid = Convert.ToInt32(CmbRole.SelectedValue);

                // ПРоверка данных на null и пустую строку
                bool result = CheckData(_FurstName, _SurName, _LastName, _Phone, _Password, _Roleid);
                // Если результат false ничего не происходит
                if (result == false)
                {
                    return;
                }
                // Создаем объект user
                Models.Users user = new Models.Users()
                {
                    FurstName = _FurstName,
                    SurName = _SurName,
                    LastName = _LastName,
                    Phone = _Phone,
                    Password = _Password,
                    RoleId = _Roleid
                };

                // Добавляем в таблицу Users объект пользователя 
                ConnectDb.Connect.Users.Add(user);
                // Сохраняем объект в базе данных
                ConnectDb.Connect.SaveChanges();

                MessageBox.Show("Запись создана успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);


                // Обновляем таблицу с пользователями
                LoadDataUsers();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Я кнопка удаления");
        }

        private bool CheckData(string _FurstName, string _SurName, string _LastName, string _Phone, string _Password, int _Roleid)
        {
            
            MessageBox.Show("Отстутствует значение в таблице имя пользователя");
            return false;
        }
    }
}
