using MailClient.Enum;

namespace MailClient.ViewModel.Interface
{
    public interface IPageViewModel : IPageClearable
    {
        string PageName { get; }
        PageNumber PageNumber { get; }        
    }
}
