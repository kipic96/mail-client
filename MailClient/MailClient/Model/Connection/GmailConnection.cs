namespace MailClient.Model.Connection
{
    class GmailConnection : BaseConnection
    {
        public GmailConnection()
        {
            SendingServerName = "smtp.gmail.com";
            SendingServerPort = 587;
            ReceivingServerName = "imap.gmail.com";
            ReceivingServerPort = 993;
        }        
    }
}
