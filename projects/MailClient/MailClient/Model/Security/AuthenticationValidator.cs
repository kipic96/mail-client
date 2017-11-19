namespace MailClient.Model.Security
{
    public static class AuthenticationValidator
    {
        public static bool Authenticate(MailBox mailBox)
        {
            return mailBox.Authenticate();
        }
    }
}
