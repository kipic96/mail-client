using MailClient.Model.Interface;

namespace MailClient.Model.InterfaceImplementation
{
    class MailCredentials : IMailCredentials
    {
        public IServerCredentials Receiving { get; set; } = new ServerCredentials();

        public IServerCredentials Sending { get; set; } = new ServerCredentials();
    }
}
