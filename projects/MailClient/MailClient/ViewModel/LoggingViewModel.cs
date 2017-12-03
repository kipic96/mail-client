using MailClient.Model.Entity;
using MailClient.ViewModel.Base;
using System;
using System.Security;
using System.Windows.Input;


namespace MailClient.ViewModel
{
    class LoggingViewModel : BaseViewModel
    {
        private string _login;
        private Enum.EmailMode _emailMode = Enum.EmailMode.Undefined;
        private bool[] _emailModeTable = new bool[] { false, false, false };
        private ICommand _logInCommand;

        public string PageNameLogIn { get; private set; } = Dictionary.PageName.Logging;
       
        public LoggingViewModel()
        {
            PageName = Dictionary.PageName.LogOut;
            PageType = Enum.PageType.Logging;
        }

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


        public Enum.EmailMode EmailMode
        {
            get
            {
                for (int i = 0; i < EmailModeTable.Length; i++)
                    if (EmailModeTable[i])
                        return (Enum.EmailMode)(i + 1);
                return Enum.EmailMode.Undefined;
            }
            set
            {
                _emailMode = value;
            }
        }

        public Action<User> LogInAction { get; set; }


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

        public void LogIn()
        {
            if (Security.EmailValidator.Validate(Login))
            {
                LogInAction(new User { Login = Login,Password = SecurePassword, EmailMode = EmailMode });
            }
            else
            {
                Log.LogMessage.Show(Dictionary.LogMessage.WrongEmailAdress);
            }      
        }

        private bool LogInValidation()
        {
            return (EmailMode != Enum.EmailMode.Undefined && Login != string.Empty && SecurePassword.Length != 0);
        }
    }
}
