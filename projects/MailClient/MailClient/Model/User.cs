using System.Security;
using MailClient.Enum;

namespace MailClient.Model
{
    public class User
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

        public User()
        {
        }

        #endregion

    }
}
