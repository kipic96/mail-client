using MailClient.Model.Connection;
using MailClient.Model.Entity;
using System.Collections.Generic;

namespace MailClient.Model
{
    public abstract class BaseMechanism
    {
        protected User _user { get; set; }
        protected BaseConnection _mailConnection { get; set; }

        public abstract void Send(Mail mail);
        public abstract IEnumerable<Mail> Receive();
        public abstract bool Authenticate();
    }
}
