using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;

namespace MailClient.Operation
{
    class GmailOperation : IMailOperationStrategy
    {
        public IEnumerable<Mail> Receive(User user)
        {
            // TODO implement for gmail
            throw new NotImplementedException();
        }

        public void Send(User user, Mail mail)
        {
            //TODO this
            throw new NotImplementedException();
        }
    }
}
