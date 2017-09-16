using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Model
{
    class Mail
    {
        #region properties

        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        #endregion
    }
}
