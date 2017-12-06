using MailClient.Enum;

namespace MailClient.Model.Connection
{
    static class ConnectionFactory
    {
        public static BaseConnection Create(EmailMode emailMode)
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
                    return BaseConnection.EmptyConnection;
                default:
                    return BaseConnection.EmptyConnection;                  
            }
        }
    }
}
