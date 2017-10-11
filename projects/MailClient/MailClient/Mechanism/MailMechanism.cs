using AE.Net.Mail;
using MailClient.Model;
using MailClient.Model.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MailClient.Mechanism
{
    class MailMechanism : IMailMechanism
    {
        public MailMechanism(User user, IMailConnection mailConnetion)
        {
            User = user;
            MailConnection = mailConnetion;
        }

        public IMailConnection MailConnection { get; }

        public User User { get; }

        public IEnumerable<Mail> Receive()
        {
            List<Mail> receivedMails = new List<Mail>();
            // converting SecureString to string
            string userPassword = new NetworkCredential(string.Empty, User.Password).Password;
            using (var imapClient = new ImapClient(
                MailConnection.Credentials.Receiving.ServerName,
                User.Login,
                userPassword,
                AuthMethods.Login,
                MailConnection.Credentials.Receiving.ServerPort,
                MailConnection.UseSsl))
            {
                imapClient.SelectMailbox(MailConnection.MailboxName);
                // max number of received mails is default 100
                int messageCount = Math.Min(imapClient.GetMessageCount(), MailConnection.MaxNumberOfReceivedMails);
                var mailMessages = imapClient.GetMessages(0, messageCount, MailConnection.HeadersOnly);
                int mailIndex = messageCount - 1;

                foreach (var mailMessage in mailMessages)
                {
                    Mail mail = Mail.Parse(mailMessage, mailIndex);
                    receivedMails.Add(mail);
                    mailIndex--;
                }
            }
            receivedMails.Reverse();
            return receivedMails;
        }

        public void Send(Mail mail)
        {
            var message = new System.Net.Mail.MailMessage(User.Login, mail.To);
            message.Subject = mail.Subject;
            message.Body = mail.Message;

            var mailer = new SmtpClient
                (MailConnection.Credentials.Sending.ServerName,
                 MailConnection.Credentials.Sending.ServerPort);
            mailer.Credentials = new NetworkCredential(User.Login, User.Password);
            mailer.EnableSsl = MailConnection.UseSsl;
            try
            {
                mailer.Send(message);
            }
            catch (Exception ex)
            {
                Log.LogMessage.Show(ex.Message);
            }
        }

        public bool Authenticate()
        {
            string userPassword = new NetworkCredential(string.Empty, User.Password).Password;
            try
            {
                using (var imapClient = new ImapClient(
                MailConnection.Credentials.Receiving.ServerName,
                User.Login,
                userPassword,
                AuthMethods.Login,
                MailConnection.Credentials.Receiving.ServerPort,
                MailConnection.UseSsl))
                {
                    // if no exception thrown authetication positive
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
