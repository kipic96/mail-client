namespace MailClient.Interface
{
    public interface IServerCredentials
    {
        string ServerName { get; set; }
        int ServerPort { get; set; }
    }
}
