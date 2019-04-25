using MailClient.Model.Connection;
using MailClient.Model.Entity;
using System.Collections.Generic;

namespace MailClient.Model
{
    public interface BaseMechanism
    {
        string Send(Mail mail, string logName);
        IEnumerable<Mail> Receive();
        bool Authenticate();
    }
}
