using System.Windows;

namespace MoCore_1_0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Implement your sign-in logic here
            if (IsValidCredentials(username, password))
            {
                MessageBox.Show("Sign-in successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement your reset password logic here
            MessageBox.Show("Password reset link has been sent to your email.", "Reset Password", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidCredentials(string username, string password)
        {
            // Implement your validation logic here
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
    }
}
