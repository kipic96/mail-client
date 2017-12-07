using System.Net;
using System.Security;


namespace MailClient.Extension
{
    public static class ExtensionSecureString
    {
        public static string MakeItString(this SecureString source)
        {
            return new NetworkCredential(string.Empty, source).Password;
        }
    }
}
