using MailClient.Interface;

namespace MailClient.HelperClass
{
    class ServerCredentials : IServerCredentials
    {
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
    }
}
