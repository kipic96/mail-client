using System.Collections.Generic;
using MailClient.Model;

namespace MailClient.Interface
{
    interface IMailOperationStrategy
    {
        IServerCredentials SendingCredentials { get; }
        IServerCredentials ReceivingCredentials { get; }

        void Send(User user, Mail mail);
        IEnumerable<Mail> Receive(User user);
    }
}
