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
using System.Data.Entity;
using System.Security.Cryptography;
using System.Windows.Navigation;


namespace WpfApp2
{

    public partial class AdminWindow : Window
    {


       
        

        private string _FurstName;
        private string _SurName;
        private string _LastName;
        private string _Phone;
        private string _Login;
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
            AdminWin.ItemsSource = ConnectDb.Connect.Users.Include(u => u.Roles).ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _FurstName = TxbFurstName.Text;
                _SurName = TxbSurName.Text;
                _LastName = TxbLastName.Text;
                _Phone = TxbPhone.Text;
                _Login = TxbLogin.Text;
                _Password = PsbPassword.Password;
                _RepeatPassword = PsbPasswordRepeat.Password;
                _Roleid = Convert.ToInt32(CmbRole.SelectedValue);

                
                bool result = CheckData(_FurstName, _SurName, _LastName, _Phone, _Login, _Password, _Roleid);
                
                if (result == false)
                {
                    return;
                }
                
                Models.Users user = new Models.Users()
                {
                    FurstName = _FurstName,
                    SurName = _SurName,
                    LastName = _LastName,
                    Phone = _Phone,
                    Login = _Login,
                    Password = _Password,
                    RoleId = _Roleid
                };


                if(_Password != _RepeatPassword)
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsLoginAlreadyExists(_Login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsPhoneAlreadyExist(_Phone))
                {
                    MessageBox.Show("Пользователь с таким номером телефона уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ConnectDb.Connect.Users.Add(user);
                
                ConnectDb.Connect.SaveChanges();

                MessageBox.Show("Запись создана успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);


                
                LoadDataUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TxbFurstName.Clear();
            TxbSurName.Clear();
            TxbLastName.Clear();
            TxbPhone.Clear();
            TxbLogin.Clear();
            PsbPassword.Clear();
            PsbPasswordRepeat.Clear();
            CmbRole.SelectedItem = null;

        }

        private bool IsPhoneAlreadyExist(string _Phone)
        {
            var user = ConnectDb.Connect.Users.FirstOrDefault(
                x => x.Phone == _Phone);
            return user != null;
        }

        private bool IsLoginAlreadyExists(string _Login)
        {
            var user = ConnectDb.Connect.Users.FirstOrDefault(
                    x => x.Login == _Login );
            return user != null;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AdminWin.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                
                var selectedUser = (Models.Users)AdminWin.SelectedItem;

                
                var userToDelete = ConnectDb.Connect.Users.Find(selectedUser.Id);
                if (userToDelete == null)
                {
                    MessageBox.Show("Пользователь не найден в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                ConnectDb.Connect.Users.Remove(userToDelete);
                ConnectDb.Connect.SaveChanges();

                
                LoadDataUsers();
                MessageBox.Show("Пользователь удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckData(string _FurstName, string _SurName, string _LastName, string _Phone, string _Login, string _Password, int _Roleid)
        {
            if (string.IsNullOrEmpty(_FurstName))
            {
                MessageBox.Show("Отсутствует значение в таблице имя пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(_SurName))
            {
                MessageBox.Show("Отсутствует значение в таблице фамилия пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(_LastName))
            {
                MessageBox.Show("Отсутствует значение в таблице отчество пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(_Phone))
            {
                MessageBox.Show("Отсутствует значение в таблице телефон пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(_Login))
            {
                MessageBox.Show("Отсутствует значение в таблице логин пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(_Password))
            {
                MessageBox.Show("Отсутствует значение в таблице пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (_Roleid <= 0)  
            {
                MessageBox.Show("Не выбрана роль пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
