using MailClient.Model.Entity;
using MailClient.ViewModel.Base;
using System;
using System.Windows.Input;

namespace MailClient.ViewModel
{
    class SendViewModel : BaseViewModel
    {
        private ICommand _sendCommand;
        private Mail _mail;

        public SendViewModel()
        {
            PageType = Enum.PageType.Send;
            PageName = Properties.Resources.Send;
        }

        public Mail Mail
        {
            get
            {
                if (_mail == null)
                    _mail = new Mail { };
                return _mail;
            }
            set
            {
                _mail = new Mail()
                {
                    Id = value.Id,
                    Subject = value.Subject,
                    From = value.From,
                    Message = value.Message,
                    To = value.To
                };
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
                        p =>
                        {
                            return (!string.IsNullOrEmpty(Mail.To)
                                && !string.IsNullOrEmpty(Mail.Subject)
                                && !string.IsNullOrEmpty(Mail.Message));
                        });
                }
                return _sendCommand;                
            }
        }

        public Action<Mail> SendMail { get; set; }

        public Func<string, bool> ValidateEmail { get; set; }

        private void Send()
        {
            if (ValidateEmail(Mail.To))
            {
                SendMail(Mail);
                Mail = new Mail { };
            }
            else
            {
                Log.LogMessage.Show(Properties.Resources.WrongEmailAdress);
            }            
        }
    }
}
