using MailClient.Model.Interface;
using MailClient.Model.InterfaceImplementation;

namespace MailClient.Model.Connection
{
    class O2Connection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }
        public string MailboxName { get; }
        public bool HeadersOnly { get; }
        public int MaxNumberOfReceivedMails { get; }

        public O2Connection()
        {
            Credentials.Sending.ServerName = "poczta.o2.pl";
            Credentials.Sending.ServerPort = 465;
            Credentials.Receiving.ServerName = "poczta.o2.pl";
            Credentials.Receiving.ServerPort = 993;
            UseSsl = true;
            MailboxName = "INBOX";
            HeadersOnly = false;
            MaxNumberOfReceivedMails = 100;
    }
    }
}
