using System.Windows;
using System.Windows.Controls;

namespace MailClient.Logging
{
    /// <summary>
    /// Interaction logic for LoggingView.xaml
    /// </summary>
    public partial class LoggingView : Page
    {
        public LoggingView()
        {
            InitializeComponent();
        }

        private void boxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            { ((dynamic)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
