using System.Security;
using MailClient.Enum;
using MailClient.Model.Interface;

namespace MailClient.Model
{
    public class User : IUser
    {
        #region properties

        public string Login { get; set; }
        public SecureString Password { get; set; }
        public EmailMode EmailMode { get; set; } = EmailMode.Undefined;

        #endregion

        #region constructors

        public User(string login, SecureString password, EmailMode emailMode)
        {
            Login = login;
            Password = password;
            EmailMode = emailMode;
        }

        #endregion

    }
}
