using MailClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Interface
{
    interface IUser
    {
        string Login { get; set; }
        SecureString Password { get; set; }
        EmailMode EmailMode { get; set; }
    }
}
