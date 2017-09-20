using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Operation;
using System.Windows;

namespace MailClient.Model
{
    class MailBox
    {
        #region constructors

        public MailBox(User user)
        {
            _user = user;
            _mailOperationStrategy = OperationFactory.Create(user.EmailMode);
        }

        #endregion

        #region fields

        private User _user;
        private IMailOperationStrategy _mailOperationStrategy;

        #endregion

        #region properties

        public IMailOperationStrategy MailOperationStrategy { get { return _mailOperationStrategy; } set { _mailOperationStrategy = value; } }
        public User User { get { return _user; } set { _user = value; } }

        #endregion

        #region methods

        public void Send(Mail mail)
        {
            MailOperationStrategy.Send(_user, mail);            
        }

        public IEnumerable<Mail> Receive()
        {
            MessageBox.Show("Receiving emails");
            return MailOperationStrategy.Receive(_user);
        }

        #endregion

    }
}
