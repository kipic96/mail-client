using System.Collections.Generic;
using MailClient.Model;

namespace MailClient.Interface
{
    public interface IMailOperationStrategy
    {
        IServerCredentials SendingCredentials { get; }
        IServerCredentials ReceivingCredentials { get; }
        bool UseSsl { get; }

        void Send(User user, Mail mail);
        IEnumerable<Mail> Receive(User user);
    }
}
