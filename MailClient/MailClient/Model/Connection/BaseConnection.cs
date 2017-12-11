namespace MailClient.Model.Connection
{
    public abstract class BaseConnection
    {
        public bool UseSsl { get; } = true;
        public string MailboxName { get; } = "INBOX";
        public bool HeadersOnly { get; } = false;
        public int MaxNumberOfReceivedMails { get; } = 100;

        public string SendingServerName { get; protected set; }
        public int SendingServerPort { get; protected set; }
        public string ReceivingServerName { get; protected set; }
        public int ReceivingServerPort { get; protected set; }

        public static readonly BaseConnection EmptyConnection =
            new NullConnection();

        private class NullConnection : BaseConnection
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
}
