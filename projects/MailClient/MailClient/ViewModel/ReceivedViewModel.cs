using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Enum;
using System.Collections.Generic;
using MailClient.Model;
using System;

namespace MailClient.ViewModel
{
    class ReceivedViewModel : BindableClass, IPageViewModel
    {
        private IEnumerable<Mail> _receivedEmails = new List<Mail>();

        public string PageName { get; } = Dictionary.PageName.Received;
        public PageNumber PageNumber { get; } = PageNumber.Received;

        public IEnumerable<Mail> ReceivedEmails
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
