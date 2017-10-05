namespace MailClient.Model.Interface
{
    public interface IMail
    {
        string To { get; set; }
        string Subject { get; set; }
        string Message { get; set; }
    }
}
