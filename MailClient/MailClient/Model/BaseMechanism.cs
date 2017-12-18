using MailClient.Model.Connection;
using MailClient.Model.Entity;
using System.Collections.Generic;

namespace MailClient.Model
{
    public interface BaseMechanism
    {
        void Send(Mail mail);
        IEnumerable<Mail> Receive();
        bool Authenticate();
    }
}
