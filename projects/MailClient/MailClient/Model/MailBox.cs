using System.Collections.Generic;
using MailClient.Interface;
using MailClient.Operation;
using System.Windows;

namespace MailClient.Model
{
    public class MailBox
    {
        #region constructors

        public MailBox(User user)
        {
            _user = user;
            _mailOperationStrategy = OperationFactory.Create(user.EmailMode);
        }

        private MailBox()
        {

        }

        #endregion

        #region fields

        private User _user;
        private IMailOperationStrategy _mailOperationStrategy;

        #endregion

        #region methods

        public void Send(Mail mail)
        {
            _mailOperationStrategy.Send(_user, mail);            
        }

        public IEnumerable<Mail> Receive()
        {
            MessageBox.Show("Receiving emails");
            IEnumerable<Mail> receivedEmails;
            receivedEmails = _mailOperationStrategy.Receive(_user);
            return receivedEmails;
        }

        #endregion

    }
}
