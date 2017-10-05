namespace MailClient.Model.Interface
{
    public interface IMailCredentials
    {
        IServerCredentials Sending { get; set; }
        IServerCredentials Receiving { get; set; }        
    }
}
