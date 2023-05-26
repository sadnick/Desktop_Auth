using Desktop_Auth.Base;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Desktop_Auth.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private string accessCode; ///Поле для хранения сгенерированного кода доступа
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Button_InputClick(object sender, RoutedEventArgs e)
        {
            string employeeNumber = TxBNumber.Text;
            string password = TxBPassword.Text;

            var currentUser = AppData.db.Authorizations.FirstOrDefault(u => u.Number == TxBNumber.Text && u.Password == TxBPassword.Text);
            if (currentUser != null && currentUser.Password == password)
            {
                // Генерируем код доступа
                accessCode = GenerateAccessCode();

                // Открываем модальное окно с сгенерированным кодом доступа
                MessageBox.Show($"Ваш код доступа: {accessCode}", "Код доступа", MessageBoxButton.OK, MessageBoxImage.Information);

                TxBCode.IsEnabled = true;
                TxBCode.Focus();
            }
            else
            {
                MessageBox.Show("Неправильный номер сотрудника или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateAccessCode()
        {
            // Генерируем код доступа (8 символов, латиница, верхний и нижний регистр, спецсимвол, цифра)
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            Random random = new Random();
            StringBuilder accessCodeBuilder = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(0, characters.Length);
                accessCodeBuilder.Append(characters[index]);
            }

            return accessCodeBuilder.ToString();
        }

        private void Button_UpdateClick(object sender, RoutedEventArgs e) ///Кнопка обновление кода
        {
            MessageBox.Show("Обновление кода!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }        

       
        private void TxBCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string enteredCode = TxBCode.Text;
                if (enteredCode == accessCode)
                {
                    // Переходим на другую страницу после правильного ввода кода доступа
                    NavigationService.Navigate(new RolePage());
                }
                else
                {
                    MessageBox.Show("Неправильный код доступа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void Button_CancelClick(object sender, RoutedEventArgs e)
        {
            TxBNumber.Text = ""; // Очистить поле ввода номера
            TxBPassword.Text = ""; // Очистить поле ввода пароля
            TxBCode.Text = "";
            MessageBox.Show("Вы отменили ввод", "Уведомление", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        private bool CheckEmployeeNumber(string employeeNumber)
        {
            var currentUser = AppData.db.Authorizations.FirstOrDefault(u => u.Number == employeeNumber);
            return currentUser != null;
        }
        private void EmployeeNumberTxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string employeeNumber = TxBNumber.Text;

                // Проверяем наличие номера сотрудника в базе данных
               if (CheckEmployeeNumber(employeeNumber))
                {
                    TxBPassword.IsEnabled = true;
                    TxBPassword.Focus(); // Устанавливаем фокус на поле ввода пароля
                }
                else
                {
                    MessageBox.Show("Введённый номер не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
