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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

        }
        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var appointmentType = (AppointmentTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var appointmentDate = AppointmentDatePicker.SelectedDate;
            var userFurstName = UserFurstNameTextBox.Text;
            var userSurName = UserSurNameTextBox.Text;
            var userLastName = UserLastNameTextBox.Text;
            

            if (appointmentDate == null || string.IsNullOrEmpty(userFurstName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (userLastName == null || string.IsNullOrEmpty(userLastName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (userLastName == null || string.IsNullOrEmpty(userSurName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"Запись на {appointmentType} успешно создана на\n{appointmentDate.Value.ToShortDateString()} для {userSurName} {userFurstName} {userLastName}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}