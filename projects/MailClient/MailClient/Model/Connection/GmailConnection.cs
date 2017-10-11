using System;
using MailClient.Model.Interface;
using MailClient.Model.InterfaceImplementation;

namespace MailClient.Model.Connection
{
    class GmailConnection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }
        public string MailboxName { get; }
        public bool HeadersOnly { get; }
        public int MaxNumberOfReceivedMails { get; }

        public GmailConnection()
        {
            Credentials.Sending.ServerName = "smtp.gmail.com";
            Credentials.Sending.ServerPort = 587;
            Credentials.Receiving.ServerName = "imap.gmail.com";
            Credentials.Receiving.ServerPort = 993;
            UseSsl = true;
            MailboxName = "INBOX";
            HeadersOnly = false;
            MaxNumberOfReceivedMails = 100;
        }        
    }
}
