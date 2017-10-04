using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class SendViewModel : BindableClass, IPageViewModel
    {
        private ICommand _sendCommand;

        public string PageName { get; } = Dictionary.PageName.Send;

        public PageNumber PageNumber { get; } = Enum.PageNumber.Send;

        public Mail Mail { get; set; } = new Mail();

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

        public Action<Mail> SendMail { get; set; }

        private void Send()
        {
            SendMail(Mail);
            Mail = new Mail();
        }

        private bool ValidateSend()
        {
            return (Mail.To != string.Empty && Mail.Subject != string.Empty && Mail.Message != string.Empty);
        }

    }
}
