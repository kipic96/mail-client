using MailClient.HelperClass;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using MailClient.Model;
using MailClient.ViewModel.Interface;

namespace MailClient.ViewModel
{
    public class ReceivedViewModel : BindableClass, IPageViewModel, IPageClearable
    {
        private ObservableCollection<Mail> _receivedMails;
        private ICommand _receivedMailsCommand;
        private ICommand _mailChooseCommand;

        public string PageName { get; } = Dictionary.PageName.Received;
        public Enum.PageNumber PageNumber { get; } = Enum.PageNumber.Received;

        public ObservableCollection<Mail> ReceivedMails
        {
            get
            {
                return _receivedMails;
            }
            set
            {
                _receivedMails = value;
                RaisePropertyChanged(nameof(ReceivedMails));
            }
        }

        public ICommand ReceiveMailsCommand
        {
            get
            {
                if (_mailChooseCommand == null)
                {
                    _mailChooseCommand = new RelayCommand(
                        p => ReceiveMailsAndSafe(),
                        p => ValidateReceiveMailsAndSafe());
                }
                return _mailChooseCommand;
            }
        }

        public ICommand MailChooseCommand
        {
            get
            {
                if (_receivedMailsCommand == null)
                {
                    _receivedMailsCommand = new RelayCommand(
                        mail => MailChoose((int)mail),
                        mail => ValidateMailChoose());
                }
                return _receivedMailsCommand;
            }
        }

        public void Clear()
        {
            if (ReceivedMails != null)
                ReceivedMails.Clear();
        }

        private void MailChoose(int mailId)
        {
            MailChoosen(mailId);
        }

        private bool ValidateMailChoose()
        {
            return true;
        }

        private void ReceiveMailsAndSafe()
        {
            IEnumerable<Mail> receivedMails = ReceiveMails();
            ReceivedMails = new ObservableCollection<Mail>(receivedMails);
        }

        private bool ValidateReceiveMailsAndSafe()
        {
            return true;
        }

        public Func<IEnumerable<Mail>> ReceiveMails { get; set; }
        public Action<int> MailChoosen { get; set; }
    }
}
