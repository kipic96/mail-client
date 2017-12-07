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

        public MailBox()
        {
        }

        public void Send(Mail mail)
        {
            _mailMechanism.Send(mail);            
        }

        public IEnumerable<Mail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public bool ValidateEmail(string email)
        {
            return Validator.EmailValidator.Validate(email);
        }

        public bool Authenticate()
        {
            return _mailMechanism.Authenticate();
        }
    }
}
