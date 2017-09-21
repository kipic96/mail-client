using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;
using MailClient.HelperClass;
using System.Net.Mail;
using System.Net;
using System.Windows;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Text;


namespace MailClient.Operation
{
    class GmailOperation : IMailOperationStrategy
    {
        private IServerCredentials _sendingCredentials = new ServerCredentials();
        private IServerCredentials _receivingCredentials = new ServerCredentials();

        public GmailOperation()
        {
            _sendingCredentials.ServerName = "smtp.gmail.com";
            _sendingCredentials.ServerPort = 587;
            _receivingCredentials.ServerName = "pop.gmail.com";
            _receivingCredentials.ServerPort = 995;
        }

        public bool UseSsl { get; } = true;

        public IEnumerable<Mail> Receive(User user)
        {
            using (Pop3Client client = new Pop3Client())
            {
                try
                {
                    client.Connect(
                        _receivingCredentials.ServerName,
                        _receivingCredentials.ServerPort,
                        UseSsl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                try
                {
                    // password not secure string anymore
                    client.Authenticate(
                    user.Login,
                    new NetworkCredential(string.Empty, user.Password).Password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    // normal throw; doesnt work 
                    return null;
                }

                int messageCount = client.GetMessageCount();

                IList<Mail> receivedEmails = new List<Mail>(messageCount);

                Message tempMessage;
                StringBuilder messageTextBuilder = new StringBuilder();

                // Messages are numbered in the interval: [1, messageCount]
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    tempMessage = client.GetMessage(i);
                    MessagePart plainText = tempMessage.FindFirstPlainTextVersion();
                    if (plainText != null)
                    {
                        messageTextBuilder.Append(plainText.GetBodyAsText());
                    }
                    Mail mail = new Mail(
                        tempMessage.Headers.From.ToString(),
                        tempMessage.Headers.To[0].ToString(), 
                        tempMessage.Headers.Subject,
                        messageTextBuilder.ToString());
                    receivedEmails.Add(mail);
                }
                return receivedEmails;

            }
        }

        public void Send(User user, Mail mail)
        {
            var message = new MailMessage(user.Login, mail.Receiver);
            message.Subject = mail.Title;
            message.Body = mail.Message;

            SmtpClient mailer = new SmtpClient
                (_sendingCredentials.ServerName, 
                _sendingCredentials.ServerPort);
            mailer.Credentials = new NetworkCredential(user.Login, user.Password);
            mailer.EnableSsl = UseSsl;
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
