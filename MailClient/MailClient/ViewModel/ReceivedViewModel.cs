using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using MailClient.ViewModel.Base;
using MailClient.Model.Entity;

namespace MailClient.ViewModel
{
    public class ReceivedViewModel : BaseViewModel
    {
        private ObservableCollection<Mail> _receivedMails;
        private ICommand _receivedMailsCommand;
        private ICommand _mailChooseCommand;

        public ReceivedViewModel()
        {
            PageName = Properties.Resources.Received;
            PageType = Enum.PageType.Received;
        }

        public Func<IEnumerable<Mail>> ReceiveMails { get; set; }
        public Action<int> MailChoosen { get; set; }

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
                        p => SaveReceivedMails(),
                        p => { return true; });
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
                        mailId => MailChoosen((int)mailId),
                        mailId => { return true; });
                }
                return _receivedMailsCommand;
            }
        }

        private void SaveReceivedMails()
        {
            ReceivedMails = new ObservableCollection<Mail>(ReceiveMails());
        }       
    }
}
