using AE.Net.Mail;
using MailClient.Extension;
using MailClient.Model.Connection;
using MailClient.Model.Entity;
using MailClient.Model.Parser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MailClient.Model
{
    class MailMechanism : BaseMechanism
    {
        private User _user { get; set; }
        private BaseConnection _mailConnection { get; set; }

        public MailMechanism(User user, BaseConnection mailConnetion)
        {
            _user = user;
            _mailConnection = mailConnetion;
        }

        public IEnumerable<Mail> Receive()
        {
            List<Mail> receivedMails = new List<Mail>();
            // converting SecureString to string
            string userPassword = _user.Password.MakeItString();
            
            using (var imapClient = new ImapClient(
                _mailConnection.ReceivingServerName,
                _user.Login,
                userPassword,
                AuthMethods.Login,
                _mailConnection.ReceivingServerPort,
                _mailConnection.UseSsl))
            {
                imapClient.SelectMailbox(_mailConnection.MailboxName);
                // max number of received mails default is 100
                int messageCount = 
                    Math.Min(imapClient.GetMessageCount(), 
                            _mailConnection.MaxNumberOfReceivedMails);
                var mailMessages = imapClient.GetMessages(
                    0, messageCount, 
                    _mailConnection.HeadersOnly);
                int mailIndex = messageCount - 1;

                foreach (var mailMessage in mailMessages)
                {
                    Mail mail = MailParser.Parse(mailMessage, mailIndex);
                    receivedMails.Add(mail);
                    mailIndex--;
                }
            }
            receivedMails.Reverse();
            return receivedMails;
        }

        public string Send(Mail mail)
        {
            var message = new System.Net.Mail.MailMessage(_user.Login, mail.To);
            message.Subject = mail.Subject;
            message.Body = mail.Message;
            if (mail.Attachment1 != null && !string.IsNullOrWhiteSpace(mail.Attachment1.FileName))
                message.Attachments.Add(new System.Net.Mail.Attachment(mail.Attachment1.FileName));

            if (mail.Attachment2 != null && !string.IsNullOrWhiteSpace(mail.Attachment2.FileName))
                message.Attachments.Add(new System.Net.Mail.Attachment(mail.Attachment2.FileName));

            if (mail.Attachment3 != null && !string.IsNullOrWhiteSpace(mail.Attachment3.FileName))
                message.Attachments.Add(new System.Net.Mail.Attachment(mail.Attachment3.FileName));

            var smtpClient = new SmtpClient
                (_mailConnection.SendingServerName,
                 _mailConnection.SendingServerPort);
            smtpClient.Credentials = new NetworkCredential(
                _user.Login, _user.Password);
            smtpClient.EnableSsl = _mailConnection.UseSsl;
            smtpClient.Send(message);
            return mail.To;
        }

        public bool Authenticate()
        {
            string userPassword = _user.Password.MakeItString();
            try
            {
                using (var imapClient = new ImapClient(
                _mailConnection.ReceivingServerName,
                _user.Login,
                userPassword,
                AuthMethods.Login,
                _mailConnection.ReceivingServerPort,
                _mailConnection.UseSsl))
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
