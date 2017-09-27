using MailClient.Enum;
using MailClient.Interface;

namespace MailClient.Connection
{
    static class ConnectionFactory
    {
        public static IMailConnection Create(EmailMode emailMode)
        {
            switch (emailMode)
            {
                case EmailMode.Gmail:
                    return new GmailConnection();
                case EmailMode.Outlook:
                    return new OnetConnection();
                case EmailMode.WP:
                    return new WpConnection();
                case EmailMode.Undefined:
                    return null;
                default:
                    return null;                  
            }
        }
    }
}
