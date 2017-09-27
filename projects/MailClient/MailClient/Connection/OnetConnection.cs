using MailClient.Interface;
using MailClient.InterfaceImplementation;

namespace MailClient.Connection
{
    class OnetConnection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }

        public OnetConnection()
        {
            Credentials.Sending.ServerName = "smtp.poczta.onet.pl";
            Credentials.Sending.ServerPort = 587;
            Credentials.Receiving.ServerName = "pop3.poczta.onet.pl";
            Credentials.Receiving.ServerPort = 110;
            UseSsl = true;
        }
    }
}
