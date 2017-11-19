using MailClient.Model;
using MailClient.ViewModel.Helper;
using MailClient.ViewModel.Interface;
using System;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class SendViewModel : BindableClass, IPageViewModel, IPageClearable
    {
        private ICommand _sendCommand;
        private Mail _mail;

        public string PageName { get; } = Dictionary.PageName.Send;

        public Enum.PageNumber PageNumber { get; } = Enum.PageNumber.Send;

        public Mail Mail
        {
            get
            {
                if (_mail == null)
                    _mail = new Mail();
                return _mail;
            }
            set
            {
                _mail = value;
                RaisePropertyChanged(nameof(Mail));
            }
        }


        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new RelayCommand(
                        p => Send(),
                        p => ValidateSend());
                }
                return _sendCommand;                
            }
        }

        public void Clear()
        {
            if (_mail != null)
                _mail.Clear();
        }

        public Action<Mail> SendMail { get; set; }

        private void Send()
        {
            if (Security.EmailValidator.Validate(Mail.To))
            {
                SendMail(Mail);
                Mail = new Mail();
            }
            else
            {
                Log.LogMessage.Show(Dictionary.LogMessage.WrongEmailAdress);
            }            
        }

        private bool ValidateSend()
        {
            return ((Mail.To != null && Mail.To != string.Empty) && (Mail.Subject != null && Mail.Subject != string.Empty) && (Mail.Message != null && Mail.Message != string.Empty)) ;
        }
    }
}
