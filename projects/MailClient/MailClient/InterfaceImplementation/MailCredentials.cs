using MailClient.Interface;

namespace MailClient.InterfaceImplementation
{
    class MailCredentials : IMailCredentials
    {
        public IServerCredentials Receiving { get; set; } = new ServerCredentials();

        public IServerCredentials Sending { get; set; } = new ServerCredentials();
    }
}
