using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Interface
{
    interface IMailCredentials
    {
        IServerCredentials Sending { get; set; }
        IServerCredentials Receiving { get; set; }        
    }
}
