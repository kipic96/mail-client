using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailClient.Interface;

namespace MailClient.HelperClass
{
    class ServerCredentials : IServerCredentials
    {
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
    }
}
