using System.Collections.Generic;
using MailClient.Model;

namespace MailClient.Interface
{
    public interface IMailOperationStrategy
    {
        void Send(User user, Mail mail);
        IEnumerable<Mail> Receive(User user);
    }
}
