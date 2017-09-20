using System;
using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Model;

namespace MailClient.Operation
{
    class WpOperation : IMailOperationStrategy
    {
        public IEnumerable<Mail> Receive(User user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void Send(User user, Mail mail)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
