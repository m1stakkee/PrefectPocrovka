using System;
using System.Collections.Generic;
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
using PrefectPocrovka;

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
                var data = ConnectDb.Connect.Users.FirstOrDefault(
                    x => x.Login == UsernameTextBox.Text && x.Password==PasswordBox.Password);

                if (data != null)
                {
                    MainWindow mainWindow = new MainWindow(); 
                    mainWindow.Show(); 
                    this.Close(); 
                }
                else
                {
                    LoginErrorTextBlock.Text = "Неверное имя пользователя или пароль.";

                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
