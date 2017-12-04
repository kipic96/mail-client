using MailClient.Model.Connection;
using System.Collections.Generic;

namespace MailClient.Model.Entity
{
    public class MailBox 
    {
        private BaseMechanism _mailMechanism;

        public MailBox(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }       

        public void Send(Mail mail)
        {
            _mailMechanism.Send(mail as Mail);            
        }

        public IEnumerable<Mail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public void ChangeUser(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }

        public bool Authenticate()
        {
            return _mailMechanism.Authenticate();
        }
    }
}
