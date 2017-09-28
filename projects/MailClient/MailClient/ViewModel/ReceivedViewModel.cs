using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Enum;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MailClient.Model;

namespace MailClient.ViewModel
{
    class ReceivedViewModel : BindableClass, IPageViewModel
    {
        private ReceivedMails _receivedMails;

        public string PageName { get; } = Dictionary.PageName.Received;
        public PageNumber PageNumber { get; } = PageNumber.Received;

        public ReceivedMails ReceivedMails
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

        public ReceivedViewModel(IEnumerable<Mail> collection)
        {
            _receivedMails = collection;
        }

        public static Action ReceiveEmails { get; set; }
    }
}
