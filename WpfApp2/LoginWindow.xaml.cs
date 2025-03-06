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
            string loginUser = UsernameTextBox.Text.Trim(); 
            string passUser = PsbPass.Password.Trim();

            if (string.IsNullOrEmpty(loginUser) || string.IsNullOrEmpty(passUser))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=IVAN\SQLEXPRESS;Initial Catalog=prefect_pocrovskoe_streshnego;Integrated Security=True;Encrypt=False"))
                {
                    sqlConnection.Open();

                    
                    string query = "SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        
                        cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = loginUser;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = passUser;

                        int userCount = (int)cmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Вы вошли!");
                            Hide();
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }
    }
}