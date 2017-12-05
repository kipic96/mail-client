using MailClient.Enum;
using MailClient.Model.Entity;
using MailClient.ViewModel.Base;

namespace MailClient.ViewModel
{
    public class MailViewModel : BaseViewModel
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
                _mail = new Mail
                {
                    To = value.To,
                    From = value.From,
                    Subject = value.Subject,
                    Message = value.Message,
                    Id = value.Id
                };      
                RaisePropertyChanged(nameof(Mail));
            }
        }

        public MailViewModel(Mail mail)
        {
           _mail = mail;
            PageType = PageType.Mail;
            PageName = Properties.Resources.Mail;
        }
    }
}
