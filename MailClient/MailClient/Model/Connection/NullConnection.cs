using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Model.Connection
{
    public class NullConnection : BaseConnection
    {
        public NullConnection()
        {
            SendingServerName = "";
            ReceivingServerName = "";
            SendingServerPort = 0;
            ReceivingServerPort = 0;
        }
    }
}
