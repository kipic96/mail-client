namespace MailClient.Model.Interface
{
    public interface IMailConnection
    {
        IMailCredentials Credentials { get; }
        bool UseSsl { get; }
        string MailboxName { get; }
        bool HeadersOnly { get; }
    }
}
