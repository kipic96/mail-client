using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using MailClient.Enum;

namespace MailClient.Model
{
    class User
    {
        #region properties

        public string Login { get; set; }
        public SecureString Password { get; set; }
        public EmailMode EmailMode { get; set; } = EmailMode.Undefined;

        #endregion

    }
}
