using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;

namespace MailClient.Operation
{
    class OutlookOperation : IMailOperationStrategy
    {
        public IEnumerable<Mail> Receive(User user)
        {
            // TO DO
            throw new NotImplementedException();
        }

        public void Send(User user, Mail mail)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
