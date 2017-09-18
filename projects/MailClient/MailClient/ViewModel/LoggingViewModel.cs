using MailClient.Interface;
using MailClient.HelperClass;
using System.Security;
using System.Windows.Input;
using MailClient.Enum;
using System.Windows;

namespace MailClient.ViewModel
{
    class LoggingViewModel : BindableClass, IPageViewModel
    {
        #region fields

        private string _login = string.Empty;
        private EmailMode _emailMode = EmailMode.Undefined;
        private bool[] _emailModeTable = new bool[] { false, false, false };
        private ICommand _try;

        

        #endregion

        #region properties 

        public string PageName { get; } = Dictionary.PageName.Logging;

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

        #endregion
    }
}
