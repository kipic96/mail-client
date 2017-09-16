using MailClient.Interface;
using MailClient.HelperClass;
using System.Security;
using System.Windows.Input;
using MailClient.Enum;
using MailClient;

namespace MailClient.ViewModel
{
    class LoggingViewModel : BindableClass, IPageViewModel
    {
        #region fields

        private string _login = string.Empty;
        private EmailMode _emailMode = EmailMode.Undefined;
        private bool[] _emailModeTable = new bool[] { false, false, false };
        private ICommand _logInCommand;

        #endregion

        #region properties 

        public string PageName { get; } = "Logging";

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(nameof(Login));
            }
        } 

        public SecureString SecurePassword { private get; set; }

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
                        return (EmailMode)i;
                return EmailMode.Undefined;
            }
            set
            {
                _emailMode = value;
            }
        }

        #endregion

        #region commands

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

        #endregion

        #region methods

        public void LogIn()
        {
            //TODO LogIn to the email, or should be in model?
            
        }

        public bool LogInValidation()
        {
            // TODO Validation of logging
            return true;
        }
        #endregion
    }
}
