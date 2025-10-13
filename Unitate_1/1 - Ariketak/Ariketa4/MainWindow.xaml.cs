using System.Windows;

namespace Ariketa4
{
    public partial class MainWindow : Window
    {
        private const string CorrectUsername = "admin";
        private const string CorrectPassword = "1234";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (username == CorrectUsername && password == CorrectPassword)
            {
                ResultLabel.Content = $"Ongi etorri sistemara, {username}";
            }
            else
            {
                ResultLabel.Content = "Identifikatu gabeko erabiltzailea";
            }
        }

        private void LimpiarButton_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            ResultLabel.Content = string.Empty;
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}