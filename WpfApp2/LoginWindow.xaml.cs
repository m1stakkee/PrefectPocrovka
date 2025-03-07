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
using WpfApp2.Models;

namespace WpfApp2
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var user = ConnectDb.Connect.Users.FirstOrDefault(
                    x => x.Login == UsernameTextBox.Text && x.Password == PsbPass.Password);

                if (user != null)
                {

                    if (user.Roles != null)
                    {
                        string roleText = "";
                        string furstName = user.FurstName ?? "";  

                        
                        if (user.Roles.Roleid == 1)
                        {
                            roleText = "Администратор";
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            this.Close(); 
                        }
                        else if (user.Roles.Roleid == 2)
                        {
                            roleText = "Пользователь";
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close(); 
                        }
                        else
                        {
                            
                            
                            MessageBox.Show($"Неизвестная роль пользователя: {user.Roles.Roleid}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return; 
                        }

                        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                        {
                            
                            MessageBox.Show("Пожалуйста, введите логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return; 
                        }

                        if (string.IsNullOrWhiteSpace(PsbPass.Password))
                        {
                            
                            MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return; 
                        }

                        MessageBox.Show($"Здравствуйте, {furstName}!\nВы вошли как {roleText}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        
                        
                        MessageBox.Show("У пользователя не назначена роль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                   
                    MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (System.Exception ex) 
            {
                
                
                MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}