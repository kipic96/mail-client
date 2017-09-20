using MailClient.Enum;

namespace MailClient.Interface
{
    public interface IPageViewModel
    {
        string PageName { get; }
        PageNumber PageNumber { get; }
    }
}
