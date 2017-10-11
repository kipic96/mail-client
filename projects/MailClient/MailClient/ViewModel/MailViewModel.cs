using MailClient.Enum;
using MailClient.Model;
using MailClient.ViewModel.Helper;
using MailClient.ViewModel.Interface;

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
           _mail = mail;
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

        public void Clear()
        {
            if (_mail != null)
             _mail.Clear();
        }
    }
}
