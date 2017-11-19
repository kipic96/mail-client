using MailClient.Enum;
using MailClient.Model.Interface;

namespace MailClient.Model.Connection
{
    static class ConnectionFactory
    {
        public static IMailConnection Create(EmailMode emailMode)
        {
            switch (emailMode)
            {
                case EmailMode.Gmail:
                    return new GmailConnection();
                case EmailMode.O2:
                    return new O2Connection();
                case EmailMode.Interia:
                    return new InteriaConnection();
                case EmailMode.Undefined:
                    return null;
                default:
                    return null;                  
            }
        }
    }
}
