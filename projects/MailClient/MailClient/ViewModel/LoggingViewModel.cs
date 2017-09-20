using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using System;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class LoggingViewModel : BindableClass, IPageViewModel
    {
        #region fields
     

        private string _login = string.Empty;
        private EmailMode _emailMode = EmailMode.Undefined;
        private bool[] _emailModeTable = new bool[] { false, false, false };
        private ICommand _try;
        private ICommand _logInCommand;

        static public int howMany { get; set; } = 0;


        #endregion

        #region constructors 

        public LoggingViewModel()
        {
            howMany++;
        }

        #endregion

        #region properties 

        public string PageName { get; } = Dictionary.PageName.Logging;
        public PageNumber PageNumber { get; } = PageNumber.Logging;

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
                        return (EmailMode)(i + 1);
                return EmailMode.Undefined;
            }
            set
            {
                _emailMode = value;
            }
        }

        #endregion

        #region events

        public static event EventHandler ChangePage;

        #endregion

        #region commands

        public ICommand Try
        {
            get
            {
                if (_try == null)
                {
                    _try = new RelayCommand(
                        p => TryIt(),
                        p => TryValidation());
                }
                return _try;
            }
        }

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

        private void TryIt()
        {
            MessageBox.Show(Login);
            MessageBox.Show(((int)EmailMode).ToString());
        }

        private bool TryValidation()
        {
            return true;
        }

        private void LogIn()
        {
            EventHandler changePageHandler = ChangePage;
            if (changePageHandler != null)
                changePageHandler(this, new PageNumberEventArg(PageNumber.Received));
            else
                MessageBox.Show("Null is in ChangePage");

            //TODO LogIn to the email, or should be in model?
            // Check if login and password for chosen email are good
            // Go to Received Page and receive email
            MessageBox.Show(Login);
            MessageBox.Show(EmailMode.ToString());

            // _applicationViewModel.ChangeViewModel(_applicationViewModel.PageViewModels[(int)Enum.PageName.Received]);
        }

        private bool LogInValidation()
        {
            // TODO Validation of logging
            // Check if all fields are completed
            return true;
        }

        #endregion
    }
}
