using MailClient.HelperClass;
using MailClient.Interface;
using MailClient.Enum;

namespace MailClient.ViewModel
{
    class ReceivedViewModel : BindableClass, IPageViewModel
    {
        public string PageName { get; } = Dictionary.PageName.Received;
        public PageNumber PageNumber { get; } = PageNumber.Received;
    }
}
