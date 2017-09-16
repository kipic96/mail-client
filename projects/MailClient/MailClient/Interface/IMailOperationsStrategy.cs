using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailClient.Model;

namespace MailClient.Interface
{
    interface IMailOperationStrategy
    {
        void Send(User user, Mail mail);
        IEnumerable<Mail> Receive(User user);
    }
}
