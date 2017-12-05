﻿using AE.Net.Mail;
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
        public MailMechanism(User user, BaseConnection mailConnetion)
        {
            _user = user;
            _mailConnection = mailConnetion;
        }

        public override IEnumerable<Mail> Receive()
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

        public override void Send(Mail mail)
        {
            var message = new System.Net.Mail.MailMessage(_user.Login, mail.To);
            message.Subject = mail.Subject;
            message.Body = mail.Message;

            var mailer = new SmtpClient
                (_mailConnection.SendingServerName,
                 _mailConnection.SendingServerPort);
            mailer.Credentials = new NetworkCredential(
                _user.Login, _user.Password);
            mailer.EnableSsl = _mailConnection.UseSsl;
            mailer.Send(message);
        }

        public override bool Authenticate()
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