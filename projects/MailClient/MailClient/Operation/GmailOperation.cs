using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;
using MailClient.HelperClass;
using System.Net.Mail;
using System.Net;
using System.Windows;

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
            _receivingCredentials.ServerName = "imap.gmail.com";
            _receivingCredentials.ServerPort = 993;
        }

        public IServerCredentials SendingCredentials { get { return _sendingCredentials; } private set { _sendingCredentials = value; } }

        public IServerCredentials ReceivingCredentials { get { return _receivingCredentials; } private set { _sendingCredentials = value; } }


        public IEnumerable<Mail> Receive(User user)
        {
            
            /*var client = new TcpClient(ReceivingCredentials.ServerName, ReceivingCredentials.ServerPort);


            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("admin@bendytree.com", "YourPasswordHere");
            var count = client.GetMessageCount();
            Message message = client.GetMessage(count);*/
            
            return null;
            //throw new NotImplementedException();
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
            mailer.EnableSsl = true;
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
