namespace MailClient.Interface
{
    interface IServerCredentials
    {
        string ServerName { get; set; }
        int ServerPort { get; set; }
    }
}
