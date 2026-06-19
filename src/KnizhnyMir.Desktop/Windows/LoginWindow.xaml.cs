using System.Windows;
using KnizhnyMir.Desktop.Services;

namespace KnizhnyMir.Desktop
{
    /// <summary>Окно входа пользователя в систему.</summary>
    public partial class LoginWindow : Window
    {
        /// <summary>Создаёт окно входа.</summary>
        public LoginWindow()
        {
            InitializeComponent();
            LoginBox.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = AppServices.Auth.Login(LoginBox.Text, PasswordBox.Password);
            if (user is null)
            {
                ErrorText.Text = "Неверный логин или пароль. Проверьте введённые данные и повторите попытку.";
                return;
            }

            AppState.CurrentUser = user;
            AppSettings.SaveCurrentUser(user);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
