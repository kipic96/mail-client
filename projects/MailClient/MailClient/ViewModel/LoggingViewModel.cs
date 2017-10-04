using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Model;
using System;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Input;


namespace MailClient.ViewModel
{
    class LoggingViewModel : BindableClass, IPageViewModel
    {
        // delete it after testing
        private string _login = "testingemail10002@gmail.com";
        private EmailMode _emailMode = EmailMode.Undefined;
        // go to false all of them after testing
        private bool[] _emailModeTable = new bool[] { true, false, false };
        private ICommand _logInCommand;

        public PageNumber PageNumber { get; } = PageNumber.Logging;
        public string PageName { get; private set; } = Dictionary.PageName.Logging;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(nameof(Login));
            }
        }

        public SecureString SecurePassword { private get; set; } = new SecureString();


        public bool[] EmailModeTable
        {
            get { return _emailModeTable; }
            set
            {
                _emailModeTable = value;
                RaisePropertyChanged(nameof(EmailModeTable));
            }
        }


        public EmailMode EmailMode
        {
            get
            {
                for (int i = 0; i < EmailModeTable.Length; i++)
                    if (EmailModeTable[i])
                        return (EmailMode)(i + 1);
                return EmailMode.Undefined;
            }
            set
            {
                _emailMode = value;
            }
        }

        public Action<PageNumber> LogInAction { get; set; }
        public Action<IUser> LogInUserAction { get; set; }


        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null)
                {
                    _logInCommand = new RelayCommand(
                        p => LogIn(),
                        p => LogInValidation());
                }
                return _logInCommand;
            }
        }

        private void LogIn()
        {
            LogInActions();

            // Check if login and password for chosen email are good 
        }

        private bool LogInValidation()
        {
            return (EmailMode != EmailMode.Undefined && Login != string.Empty && SecurePassword.Length != 0);
        }

        private void LogInActions()
        {
            LogInUserAction(new User(Login, SecurePassword, EmailMode));
            LogInAction(PageNumber.Received);
        }
    }
}
