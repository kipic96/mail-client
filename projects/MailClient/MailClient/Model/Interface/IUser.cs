using MailClient.Enum;
using System.Security;

namespace MailClient.Model.Interface
{
    public interface IUser
    {
        string Login { get; set; }
        SecureString Password { get; set; }
        EmailMode EmailMode { get; set; }
    }
}
