using MailClient.Model.Interface;

namespace MailClient.Model.Security
{
    public static class AuthenticationValidator
    {
        public static bool Authenticate(IMailBox mailBox)
        {
            return mailBox.Authenticate();
        }
    }
}
