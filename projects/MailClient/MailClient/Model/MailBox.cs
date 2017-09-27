using MailClient.Connection;
using MailClient.Interface;
using MailClient.Mechanism;
using System.Collections.Generic;

namespace MailClient.Model
{
    public class MailBox 
    {
        public MailBox(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }

        private IMailMechanism _mailMechanism;
        

        public void Send(Mail mail)
        {
            _mailMechanism.Send(mail);            
        }

        public IEnumerable<Mail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public void ChangeUser(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }
    }
}
