using MailClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Interface
{
    interface IMailMechanism
    {
        User User { get; }
        IMailConnection MailConnection { get; }

        void Send(Mail mail);
        IEnumerable<Mail> Receive();
    }
}
