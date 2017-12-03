using System.Windows;
using System;
using System.Diagnostics;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        PageManager context;
        ApplicationView app;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                app = new ApplicationView();
                context = new PageManager();
                context.LogoutAction = OnLogout;
                app.Show();
                app.DataContext = context;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }            
        }

        void OnLogout()
        {
            try
            {
                context = new PageManager();
                context.LogoutAction = OnLogout;
                app.DataContext = context;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
