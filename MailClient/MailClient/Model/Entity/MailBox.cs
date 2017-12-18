using MailClient.Model.Connection;
using System.Collections.Generic;

namespace MailClient.Model.Entity
{
    public class MailBox 
    {
        private BaseMechanism _mailMechanism;

        public static readonly MailBox EmptyMailBox =
            new MailBoxNull();

        private MailBox() { }

        public MailBox(User user)
        {
            _mailMechanism = new MailMechanism(user, ConnectionFactory.Create(user.EmailMode));
        }        

        public virtual void Send(Mail mail)
        {
            _mailMechanism.Send(mail);            
        }

        public virtual IEnumerable<Mail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public virtual bool ValidateEmail(string email)
        {
            return Validator.EmailValidator.Validate(email);
        }

        public virtual bool Authenticate()
        {
            return _mailMechanism.Authenticate();
        }

        private class MailBoxNull : MailBox
        {
            public MailBoxNull() { }

            public MailBoxNull(User user) : base(user) { }

            public override void Send(Mail mail) { }

            public override IEnumerable<Mail> Receive() { return new List<Mail>(); }

            public override bool Authenticate() { return false; }
        }
    }
}
