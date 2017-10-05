using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Model;
using MailClient.Model.Interface;
using MailClient.ViewModel.Interface;
using System;
using System.Security;
using System.Windows.Input;


namespace MailClient.ViewModel
{
    class LoggingViewModel : BindableClass, IPageViewModel, IPageClearable
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

        public Action<IUser> LogInAction { get; set; }


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

        public void Clear()
        {
            _login = string.Empty;
            _emailModeTable = new bool[] { false, false, false };
            _emailMode = EmailMode.Undefined;
            SecurePassword.Clear();
        }

        private void LogIn()
        {
            if (Security.EmailValidator.Validate(Login))
            {
                LogInAction(new User(Login, SecurePassword, EmailMode));
            }
            else
            {
                Log.LogMessage.Show(Dictionary.LogMessage.WrongEmailAdress);
            }      
        }

        private bool LogInValidation()
        {
            return (EmailMode != EmailMode.Undefined && Login != string.Empty && SecurePassword.Length != 0);
        }
    }
}
