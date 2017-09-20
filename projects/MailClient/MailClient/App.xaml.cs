using System.Windows;
using MailClient.ViewModel;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }
    }
}
