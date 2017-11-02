using MailClient.Model.Interface;
using MailClient.Model.InterfaceImplementation;

namespace MailClient.Model.Connection
{
    class InteriaConnection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }
        public string MailboxName { get; }
        public bool HeadersOnly { get; }
        public int MaxNumberOfReceivedMails { get; }

        public InteriaConnection()
        {
            Credentials.Sending.ServerName = "poczta.interia.pl";
            Credentials.Sending.ServerPort = 587;
            Credentials.Receiving.ServerName = "poczta.interia.pl";
            Credentials.Receiving.ServerPort = 993;
            UseSsl = true;
            MailboxName = "INBOX";
            HeadersOnly = false;
            MaxNumberOfReceivedMails = 100;
        }
    }
}
