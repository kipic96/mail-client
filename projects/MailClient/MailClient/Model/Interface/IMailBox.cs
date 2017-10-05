using System.Collections.Generic;

namespace MailClient.Model.Interface
{
    public interface IMailBox
    {
        void Send(IMail mail);
        IEnumerable<IMail> Receive();
        void ChangeUser(IUser user);
        bool Authenticate();
    }
}
