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

        public IUser User { get; }

        public IEnumerable<Mail> Receive()
        {
            IList<Mail> receivedMails = new List<Mail>();
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
                var mailMessages = imapClient.GetMessages(0, imapClient.GetMessageCount(), MailConnection.HeadersOnly);
                int mailCount = imapClient.GetMessageCount() - 1;
                foreach (var mailMessage in mailMessages)
                {
                   Mail mail = Mail.Parse(mailMessage, mailCount);
                    receivedMails.Add(mail);
                    mailCount--;
                }
            }
            (receivedMails as List<Mail>).Reverse();
            return receivedMails;
        }

        public void Send(Mail mail)
        {
            var message = new System.Net.Mail.MailMessage(User.Login, mail.To);
            message.Subject = mail.Subject;
            message.Body = mail.Message;

            SmtpClient mailer = new SmtpClient
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
