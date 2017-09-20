using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;

namespace MailClient.Operation
{
    class OutlookOperation : IMailOperationStrategy
    {
        private IServerCredentials _sendingCredentials;
        private IServerCredentials _receivingCredentials;

        public OutlookOperation()
        {
            /*_sendingCredentials.ServerName = "smtp.gmail.com";
            _sendingCredentials.ServerPort = 587;
            _receivingCredentials.ServerName = "imap.gmail.com";
            _receivingCredentials.ServerPort = 993;*/
        }

        public IServerCredentials SendingCredentials { get { return _sendingCredentials; } }

        public IServerCredentials ReceivingCredentials { get { return _receivingCredentials; } }


        public IEnumerable<Mail> Receive(User user)
        {
            // TO DO 
            return null;
           // throw new NotImplementedException();
        }

        public void Send(User user, Mail mail)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
