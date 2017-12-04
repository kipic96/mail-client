namespace MailClient.Model.Connection
{
    class InteriaConnection : BaseConnection
    {
        public InteriaConnection()
        {
            SendingServerName = "poczta.interia.pl";
            SendingServerPort = 587;
            ReceivingServerName = "poczta.interia.pl";
            ReceivingServerPort = 993;
        }
    }
}
