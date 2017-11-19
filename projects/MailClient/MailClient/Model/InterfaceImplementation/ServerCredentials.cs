using MailClient.Model.Interface;

namespace MailClient.Model.InterfaceImplementation
{
    class ServerCredentials : IServerCredentials
    {
        public string ServerName { get; set; }
        public int ServerPort { get; set; }
    }
}
