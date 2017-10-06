using MailClient.Model.Connection;
using MailClient.Model.Interface;
using MailClient.Mechanism;
using System.Collections.Generic;

namespace MailClient.Model
{
    public class MailBox : IMailBox
    {
        public string UserLogin
        {
            get
            {
                if (_mailMechanism == null)
                    return string.Empty;
                return _mailMechanism.User.Login;
            }
        }

        public MailBox(IUser user)
        {
            _mailMechanism = new MailMechanism(user as User, ConnectionFactory.Create(user.EmailMode));
        }

        public MailBox()
        {
        }

        private IMailMechanism _mailMechanism;
        

        public void Send(IMail mail)
        {
            _mailMechanism.Send(mail as Mail);            
        }

        public IEnumerable<IMail> Receive()
        {
            return _mailMechanism.Receive();
        }

        public void ChangeUser(IUser user)
        {
            _mailMechanism = new MailMechanism(user as User, ConnectionFactory.Create(user.EmailMode));
        }

        public bool Authenticate()
        {
            return _mailMechanism.Authenticate();
        }
    }
}
