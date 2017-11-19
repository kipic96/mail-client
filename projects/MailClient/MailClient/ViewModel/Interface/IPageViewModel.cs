namespace MailClient.ViewModel.Interface
{
    public interface IPageViewModel : IPageClearable
    {
        string PageName { get; }
        Enum.PageNumber PageNumber { get; }        
    }
}
