using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Model;

namespace MailClient.ViewModel
{
    public class MailViewModel : BindableClass, IPageViewModel
    {
        private Mail _mail;

        public Mail Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
                RaisePropertyChanged(nameof(Mail));
            }
        }

        public MailViewModel(Mail mail)
        {
            Mail = mail;
        }

        public string PageName
        {
            get
            {
                return Dictionary.PageName.Mail;
            }
        }

        public PageNumber PageNumber
        {
            get
            {
                return PageNumber.Mail;
            }
        }
    }
}
