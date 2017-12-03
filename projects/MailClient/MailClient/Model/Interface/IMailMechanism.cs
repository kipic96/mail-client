using MailClient.Model.Entity;
using System.Collections.Generic;

namespace MailClient.Model.Interface
{
    interface IMailMechanism
    {
        User User { get; }
        IMailConnection MailConnection { get; }

        void Send(Mail mail);
        IEnumerable<Mail> Receive();
        bool Authenticate();
    }
}
