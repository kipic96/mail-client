using MailClient.Interface;
using MailClient.InterfaceImplementation;

namespace MailClient.Connection
{
    class WpConnection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }

        public WpConnection()
        {
            Credentials.Sending.ServerName = "smtp.wp.pl";
            Credentials.Sending.ServerPort = 465;
            Credentials.Receiving.ServerName = "imap.wp.pl";
            Credentials.Receiving.ServerPort = 993;
            UseSsl = true;
        }
    }
}
