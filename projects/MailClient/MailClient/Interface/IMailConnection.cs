namespace MailClient.Interface
{
    interface IMailConnection
    {
        IMailCredentials Credentials { get; }
        bool UseSsl { get; }
    }
}
