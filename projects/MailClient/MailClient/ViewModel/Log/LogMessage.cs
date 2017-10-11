using System.Windows;

namespace MailClient.ViewModel.Log
{
    public static class LogMessage
    {
        public static void Show (string logMessage)
        {
            MessageBox.Show(logMessage);
        }
    }
}
