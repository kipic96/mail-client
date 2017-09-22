using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Enum;
using System.Collections.Generic;
using MailClient.Model;
using System;
using System.Collections.ObjectModel;

namespace MailClient.ViewModel
{
    class ReceivedViewModel : BindableClass, IPageViewModel
    {
        private ObservableCollection<Mail> _receivedEmails;

        public string PageName { get; } = Dictionary.PageName.Received;
        public PageNumber PageNumber { get; } = PageNumber.Received;

        public ObservableCollection<Mail> ReceivedEmails
        {
            get
            {
                return _receivedEmails;
            }
            set
            {
                _receivedEmails = value;
                RaisePropertyChanged(nameof(ReceivedEmails));
            }
        }

        public static Action ReceiveEmails { get; set; }
    }
}
