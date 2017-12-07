using System.Security;
using MailClient.Enum;

namespace MailClient.Model.Entity
{
    public class User
    {
        public string Login { get; set; }
        public SecureString Password { get; set; }
        public EmailMode EmailMode { get; set; } = EmailMode.Undefined;
    }
}
