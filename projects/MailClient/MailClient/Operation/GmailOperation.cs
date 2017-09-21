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

        public IServerCredentials SendingCredentials
        { get
            { return _sendingCredentials; }
          private set
            { _sendingCredentials = value; }
        }

        public IServerCredentials ReceivingCredentials
        { get
            { return _receivingCredentials; }
          private set
            { _sendingCredentials = value; }
        }

        public bool UseSsl { get; } = true;

        public IEnumerable<Mail> Receive(User user)
        {
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(
                    _receivingCredentials.ServerName, 
                    _receivingCredentials.ServerPort, 
                    UseSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(
                    user.Login, 
                    new NetworkCredential(string.Empty, user.Password).Password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // We want to download all messages
                IList<Mail> allMessages = new List<Mail>(messageCount);

                Message tempMessage;
                StringBuilder builder = new StringBuilder();

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    tempMessage = client.GetMessage(i);
                    OpenPop.Mime.MessagePart plainText = tempMessage.FindFirstPlainTextVersion();
                    if (plainText != null)
                    {
                        builder.Append(plainText.GetBodyAsText());
                    }
                    Mail mail = new Mail(
                        tempMessage.Headers.From.ToString(),
                        tempMessage.Headers.To[0].ToString(), 
                        tempMessage.Headers.Subject,
                        builder.ToString());
                    allMessages.Add(mail);
                }
                return allMessages;

            }
        }

        public void Send(User user, Mail mail)
        {
            var message = new MailMessage(user.Login, mail.Receiver);
            message.Subject = mail.Title;
            message.Body = mail.Message;

            SmtpClient mailer = new SmtpClient
                (SendingCredentials.ServerName, 
                SendingCredentials.ServerPort);
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
