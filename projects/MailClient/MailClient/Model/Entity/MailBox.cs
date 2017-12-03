using MailClient.Model.Connection;
using MailClient.Model.Interface;
using MailClient.Model.Mechanism;
using System.Collections.Generic;

namespace MailClient.Model.Entity
{
    public class MailBox 
    {
        private IMailMechanism _mailMechanism;


        public string UserLogin
        {
            get
            {
                if (_mailMechanism == null)
                    return string.Empty;
                return _mailMechanism.User.Login;
            }
        }

        public MailBox()
        {

        }

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
