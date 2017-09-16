using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailClient.Enum;
using MailClient.Interface;

namespace MailClient.Operation
{
    static class OperationFactory
    {
        public static IMailOperationStrategy Create(EmailMode emailMode)
        {
            switch (emailMode)
            {
                // TODO all reactions
                case EmailMode.Gmail:
                    return new GmailOperation();
                case EmailMode.Outlook:
                    return new OutlookOperation();
                case EmailMode.WP:
                    return new WpOperation();
                case EmailMode.Undefined:
                    return null;
                default:
                    return null;                  
            }
        }
    }
}
