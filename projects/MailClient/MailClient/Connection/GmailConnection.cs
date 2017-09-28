﻿using MailClient.Interface;
using MailClient.InterfaceImplementation;

namespace MailClient.Connection
{
    class GmailConnection : IMailConnection
    {
        public IMailCredentials Credentials { get; } = new MailCredentials();
        public bool UseSsl { get; }
        public string MailboxName { get; }
        public bool HeadersOnly { get; }

        public GmailConnection()
        {
            Credentials.Sending.ServerName = "smtp.gmail.com";
            Credentials.Sending.ServerPort = 587;
            Credentials.Receiving.ServerName = "imap.gmail.com";
            Credentials.Receiving.ServerPort = 993;
            UseSsl = true;
            MailboxName = "INBOX";
            HeadersOnly = false;
        }        
    }
}