using AE.Net.Mail;
using MailClient.Interface;
using MailClient.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;

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
            string password = new NetworkCredential(string.Empty, User.Password).Password;
            using (var imapClient = new ImapClient(
                MailConnection.Credentials.Receiving.ServerName, 
                User.Login, 
                password,
                AuthMethods.Login, 
                MailConnection.Credentials.Receiving.ServerPort, 
                MailConnection.UseSsl))
            {
                imapClient.SelectMailbox(MailConnection.MailboxName);
                var mailMessages = imapClient.GetMessages(0, imapClient.GetMessageCount(), MailConnection.HeadersOnly);
                foreach (var mailMessage in mailMessages)
                {
                    receivedMails.Add(Mail.Parse(mailMessage));
                }                
            }
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
                MessageBox.Show(ex.Message);
            }

        }
    }
}
