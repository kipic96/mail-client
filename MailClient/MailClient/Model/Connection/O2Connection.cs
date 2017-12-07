namespace MailClient.Model.Connection
{
    class O2Connection : BaseConnection
    {
        public O2Connection()
        {
            SendingServerName = "poczta.o2.pl";
            SendingServerPort = 465;
            ReceivingServerName = "poczta.o2.pl";
            ReceivingServerPort = 993;
        }
    }
}
