using Prism.Commands;
using Prism.Mvvm;
using System.Security;
using System.Windows.Input;

namespace MailClient.Logging
{
    class LoggingViewModel : BindableBase, IPageViewModel
    {
        #region properties 

        public string PageName { get; } = "Logging";

        public string Login
        {
            get { return Login; }
            set
            {
                Login = value;
                RaisePropertyChanged(nameof(Login));
            }
        }

        public SecureString SecurePassword { private get; set; }

        public bool[] EmailModeTable
        {
            get { return EmailModeTable; }
            set
            {
                EmailModeTable = value;
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
        }

        #endregion

        #region commands

        public ICommand ValidateCommand { get; set; }

        #endregion

        #region constructors

        public LoggingViewModel()
        {
            ValidateCommand = new DelegateCommand(ValidateAction);
            //EmailModeTable = new bool[] { false, false, false };
        }

        #endregion

        #region actions

        public void ValidateAction()
        {

        }

        #endregion
    }
}
