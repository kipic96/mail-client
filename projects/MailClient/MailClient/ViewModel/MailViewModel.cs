using MailClient.Enum;
using MailClient.HelperClass;
using MailClient.Model;
using MailClient.Model.Interface;
using MailClient.ViewModel.Interface;

namespace MailClient.ViewModel
{
    public class MailViewModel : BindableClass, IPageViewModel
    {
        private IMail _mail;

        public IMail Mail
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

        public MailViewModel(IMail mail)
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
            (_mail as Mail).Clear();
        }
    }
}
